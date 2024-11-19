using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ShrimpPond.API.Hubs;
using ShrimpPond.API.MQTTModels;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Infrastructure.Communication;
using ShrimpPond.Domain.Environments;
using Timer = System.Timers.Timer;
using ShrimpPond.Application.Contract.GmailService;
using ShrimpPond.Application.Models.Gmail;


namespace ShrimpPond.API.Worker
{
    public class ScadaHost : BackgroundService
    {
        private readonly ManagedMqttClient _mqttClient;
        private readonly Buffer _buffer;
        private readonly IGmailSender _gmailSender;

        private readonly IHubContext<NotificationHub> _hubContext;

        //private readonly IGmailSender _gmailSender;
        //private readonly IEmailSender _emailSender;
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceScopeFactory _scopeFactory;
        private static Timer? _timer;

        public ScadaHost(ManagedMqttClient mqttClient, Buffer buffer,
            IHubContext<NotificationHub> hubContext,
            IServiceScopeFactory scopeFactory,
            IGmailSender gmailSender
            //IGmailSender gmailSender
            //IEmailSender emailSender,
            //IUnitOfWork unitOfWork
        )
        {
            _mqttClient = mqttClient;
            _buffer = buffer;
            _hubContext = hubContext;
            _gmailSender = gmailSender;
            //_gmailSender = gmailSender;
            //_emailSender = emailSender;
            //_unitOfWork = unitOfWork;
            _scopeFactory = scopeFactory;
            _timer = new Timer(1000*60);
            _timer.Elapsed += async (_, _) => await CheckTimeToStart();
            _timer.Start();

        }


        private async Task CheckTimeToStart()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var now = DateTime.UtcNow.AddHours(7);
                string dataPonds = "";

                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var ponds = unitOfWork.pondRepository.FindAll().Where(x => x.Status == Domain.PondData.EPondStatus.Active).ToList();
                foreach (var pond in ponds)
                {
                    dataPonds += pond.PondId + ",";
                }

                if (dataPonds.EndsWith(","))
                {
                    dataPonds = dataPonds.Remove(dataPonds.Length - 1);
                }


                var timeSettingId = unitOfWork.timeSettingRepository.FindAll().Count();
                var timeSettingObjects = unitOfWork.timeSettingObjectRepository.FindAll()
                    .Where(x => x.TimeSettingId == timeSettingId).ToList();

                foreach (var unused in timeSettingObjects
                             .Select(timeSettingObject => Convert.ToDateTime(timeSettingObject.Time))
                             .Where(time => now.Hour == time.Hour && now.Minute == time.Minute))
                {
                    await _mqttClient.Publish($"SHRIMP_POND/SELECT_POND", dataPonds, false);
                    Thread.Sleep(1000);
                    await _mqttClient.Publish($"SHRIMP_POND/START", "START", false);

                    var gmail = new GmailMessage
                    {
                        To = "vu34304@gmail.com",
                        Subject = "Gửi thời gian cài đặt thành công vào lúc: " + now.ToLongTimeString(),
                        Body = "Danh sách ao đo:" + dataPonds
                    };

                    await _gmailSender.SendGmail(gmail);
                    Console.WriteLine("Đã gửi ");
                }
            }

        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ConnectToMqttBrokerAsync();
        }


        private async Task ConnectToMqttBrokerAsync()
        {
            _mqttClient.MessageReceived += OnMqttClientMessageReceivedAsync;
            await _mqttClient.ConnectAsync();
            await _mqttClient.Subscribe("SHRIMP_POND/+/+");
        }

        private async Task OnMqttClientMessageReceivedAsync(MqttMessage e)
        {
            var topic = e.Topic;
            var payloadMessage = e.Payload;
            if (topic is null || payloadMessage is null)
            {
                return;
            }

            var topicSegments = topic.Split('/');
            var topic1 = topicSegments[1];
            var topic2 = topicSegments[2];


            payloadMessage = payloadMessage.Replace("false", "\"FALSE\"");
            Thread.Sleep(1000);
            payloadMessage = payloadMessage.Replace("true", "\"TRUE\"");


            //// gửi chỉ số oee, xử lí lưu database, gửi lên web
            switch (topic1)
            {
                case "ENVIRONMENT":
                {
                    var environments = JsonConvert.DeserializeObject<List<EnviromentData>>(payloadMessage)!.ToList();

                    foreach (var environment in environments)
                    {

                        var environmentSend = new EnviromentSend
                        {
                            PondId = topic2,
                            name = environment.name,
                            timestamp = (DateTime.UtcNow.AddHours(7)).ToString("yyyy-MM-dd HH:mm:ss"),
                            value = environment.value,
                        };

                        string jsonEnvironment = JsonConvert.SerializeObject(environmentSend);

                        await _hubContext.Clients.All.SendAsync("EnvironmentChanged", jsonEnvironment);

                        //Lưu vào database
                        using var scope = _scopeFactory.CreateScope();
                        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                        var data = new EnvironmentStatus()
                        {
                            PondId = topic2,
                            Name = environment.name,
                            Value = environment.value,
                            Timestamp = (DateTime.UtcNow.AddHours(7))
                        };

                        unitOfWork.environmentStatusRepository.Add(data);
                        await unitOfWork.SaveChangeAsync();
                    }

                    break;
                }

                case "ERROR_CHECK":
                    {
                        switch (topic2)
                        {
                            case "CONNECT":
                                {   
                                    if(payloadMessage == "BAD" )
                                    {
                                        //Gui mail
                                        var gmail = new GmailMessage
                                        {
                                            To = "vu34304@gmail.com",
                                            Subject = "Mất kết nối với MQTT" ,
                                            Body = "Vui lòng kiểm tra hoặc reset !" 

                                        };

                                        await _gmailSender.SendGmail(gmail);

                                        //Gui signalR
                                        var error = new ErrorNotification(topic2, payloadMessage);

                                        string jsonEnvironment = JsonConvert.SerializeObject(error);

                                        await _hubContext.Clients.All.SendAsync("ErrorNotification", jsonEnvironment);

                                    }

                                    break;
                                }
                            case "RS485":
                                {
                                    if (payloadMessage == "BAD")
                                    {
                                        var gmail = new GmailMessage
                                        {
                                            To = "vu34304@gmail.com",
                                            Subject = "Lỗi RS485",
                                            Body = "Vui lòng kiểm tra lại !"

                                        };

                                        await _gmailSender.SendGmail(gmail);

                                        //Gui signalR
                                        var error = new ErrorNotification(topic2, payloadMessage);

                                        string jsonEnvironment = JsonConvert.SerializeObject(error);

                                        await _hubContext.Clients.All.SendAsync("ErrorNotification", jsonEnvironment);
                                    }
                                    break;
                                }
                            case "PCF8574":
                                {
                                    if (payloadMessage == "BAD")
                                    {
                                        var gmail = new GmailMessage
                                        {
                                            To = "vu34304@gmail.com",
                                            Subject = "Lỗi PCF8574",
                                            Body = "Vui lòng kiểm tra lại !"

                                        };

                                        await _gmailSender.SendGmail(gmail);

                                        //Gui signalR
                                        var error = new ErrorNotification(topic2, payloadMessage);

                                        string jsonEnvironment = JsonConvert.SerializeObject(error);

                                        await _hubContext.Clients.All.SendAsync("ErrorNotification", jsonEnvironment);
                                    }
                                    
                                    break;
                                }
                            case "ADC":
                                {
                                    if (payloadMessage == "BAD")
                                    {
                                        var gmail = new GmailMessage
                                        {
                                            To = "vu34304@gmail.com",
                                            Subject = "Lỗi ADC",
                                            Body = "Vui lòng kiểm tra lại !"

                                        };

                                        await _gmailSender.SendGmail(gmail);

                                        //Gui signalR
                                        var error = new ErrorNotification(topic2, payloadMessage);

                                        string jsonEnvironment = JsonConvert.SerializeObject(error);

                                        await _hubContext.Clients.All.SendAsync("ErrorNotification", jsonEnvironment);
                                    }
                                   
                                    break;
                                }
                        }


                        break;
                    }
            }
        }
    }
}
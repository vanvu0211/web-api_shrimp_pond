using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ShrimpPond.API.Hubs;
using ShrimpPond.API.MQTTModels;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Infrastructure.Communication;
using ShrimpPond.Domain.Environments;
using Timer = System.Timers.Timer;


namespace ShrimpPond.API.Worker
{
    public class ScadaHost : BackgroundService
    {
        private readonly ManagedMqttClient _mqttClient;
        private readonly Buffer _buffer;

        private readonly IHubContext<NotificationHub> _hubContext;

        //private readonly IGmailSender _gmailSender;
        //private readonly IEmailSender _emailSender;
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceScopeFactory _scopeFactory;
        private static Timer? _timer;

        public ScadaHost(ManagedMqttClient mqttClient, Buffer buffer,
            IHubContext<NotificationHub> hubContext,
            IServiceScopeFactory scopeFactory
            //IGmailSender gmailSender
            //IEmailSender emailSender,
            //IUnitOfWork unitOfWork
        )
        {
            _mqttClient = mqttClient;
            _buffer = buffer;
            _hubContext = hubContext;
            //_gmailSender = gmailSender;
            //_emailSender = emailSender;
            //_unitOfWork = unitOfWork;
            _scopeFactory = scopeFactory;
            _timer = new Timer(60000);
            _timer.Elapsed += async (_, _) => await CheckTimeToStart();
            _timer.Start();

            Console.WriteLine("Đang chạy...");
        }


        private async Task CheckTimeToStart()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var now = DateTime.UtcNow.AddHours(7);
                string dataPonds = "";

                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var ponds = unitOfWork.pondRepository.FindAll()
                    .Where(x => x.Status == Domain.PondData.EPondStatus.Active).ToList();
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
                    await _mqttClient.Publish($"SHRIMP_POND/SELECT_POND", dataPonds, true);
                    Thread.Sleep(1000);
                    await _mqttClient.Publish($"SHRIMP_POND/START", "START", true);
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
#pragma warning disable CS8601 // Possible null reference assignment.
                        var environmentSend = new EnviromentSend
                        {
                            PondId = topic2,
                            name = environment.name,
                            timestamp = (DateTime.UtcNow.AddHours(7)).ToString("yyyy-MM-dd HH:mm:ss"),
                            value = environment.value,
                        };
#pragma warning restore CS8601 // Possible null reference assignment.

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
            }
        }
    }
}
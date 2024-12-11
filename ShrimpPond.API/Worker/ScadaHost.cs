using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShrimpPond.API.Hubs;
using ShrimpPond.API.MQTTModels;
using ShrimpPond.Application.Contract.GmailService;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Models.Gmail;
using ShrimpPond.Domain.Environments;
using ShrimpPond.Infrastructure.Communication;
using Timer = System.Timers.Timer;


namespace ShrimpPond.API.Worker
{
    public class ScadaHost : BackgroundService
    {
        private readonly ManagedMqttClient _mqttClient;
        private readonly Buffer _buffer;
        private readonly IGmailSender _gmailSender;

        private readonly IHubContext<NotificationHub> _hubContext;

        private readonly IServiceScopeFactory _scopeFactory;
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public double unitTime { get; set; }
        public int count { get; set; }
        public int temp { get; set; } = 1;
        public DateTime time { get; set; }
        public ScadaHost(ManagedMqttClient mqttClient, Buffer buffer,
            IHubContext<NotificationHub> hubContext,
            IServiceScopeFactory scopeFactory,
            IGmailSender gmailSender

        )
        {
            _mqttClient = mqttClient;
            _buffer = buffer;
            _hubContext = hubContext;
            _gmailSender = gmailSender;
            _scopeFactory = scopeFactory;

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
                
                case "POND":
                    {
                        count = int.Parse(payloadMessage);
                        temp = 1;
                        await SendMail("vu34304@gmail.com", "Gửi danh sách ao đo thành công", "");
                        await SendMail("van048483@gmail.com", "Gửi danh sách ao đo thành công", "");
                        break;
                    }
                case "START_TIME":
                    {
                        switch (payloadMessage)
                        {
                            case "START":
                                {
                                    startTime = DateTime.UtcNow.AddHours(7);
                                    break;
                                }
                          
                        }
                        break;
                    }
                case "ENVIRONMENT":
                    {

                        if (temp <= count && startTime != null)
                        {
                            var unitTime = (DateTime.UtcNow.AddHours(7) - startTime.Value).TotalMinutes * temp/count;
                            time = startTime.Value.AddMinutes(unitTime);
                            await SendMail("vu34304@gmail.com", "unittime ", unitTime.ToString());
                            temp++;
                        }
                        else
                        {
                            temp = 1;
                            time = DateTime.UtcNow.AddHours(7);
                        }

                        var environments = JsonConvert.DeserializeObject<List<EnviromentData>>(payloadMessage)!.ToList();

                        foreach (var environment in environments)
                        {
                            //Lưu vào database
                            using var scope = _scopeFactory.CreateScope();
                            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                            var data = new EnvironmentStatus()
                            {
                                PondId = topic2,
                                Name = environment.name,
                                Value = environment.value,
                                Timestamp = time,// chia thời gian cho mỗi lần đo
                            };


                            unitOfWork.environmentStatusRepository.Add(data);
                            await unitOfWork.SaveChangeAsync();


                            //Gửi dữ liệu

                            await SendMail("vu34304@gmail.com", "Gửi dữ liệu ao " + topic2, "Dữ liệu: " + JsonConvert.SerializeObject(data));
                            await SendMail("van048483@gmail.com", "Gửi dữ liệu ao " + topic2, "Dữ liệu: " + JsonConvert.SerializeObject(data));

                            //await _hubContext.Clients.All.SendAsync("EnvironmentChanged", jsonEnvironment);

                        }

                        break;
                    }

                case "ERROR_CHECK":
                    {
                        switch (topic2)
                        {
                            case "CONNECT":
                                {
                                    if (payloadMessage == "BAD")
                                    {
                                        
                                        //await SendMail("vu34304@gmail.com", "Kết nối MQTT", "Mất kết nối MQTT");
                                        //await SendMail("van048483@gmail.com", "Kết nối MQTT", "Mất kết nối MQTT");

                                        //Gui signalR
                                        var error = new ErrorNotification(topic2, payloadMessage);

                                        string jsonEnvironment = JsonConvert.SerializeObject(error);

                                        await _hubContext.Clients.All.SendAsync("ErrorNotification", jsonEnvironment);

                                    }

                                    if (payloadMessage == "GOOD")
                                    {
                                        //Gui mail
                                        //await SendMail("vu34304@gmail.com", "Kết nối MQTT", "Đã kết nối MQTT");
                                        //await SendMail("van048483@gmail.com", "Kết nối MQTTT", "Đã kết nối MQTT");

                                        //Gui signalR
                                        var error = new ErrorNotification(topic2, payloadMessage);

                                        string jsonEnvironment = JsonConvert.SerializeObject(error);

                                        await _hubContext.Clients.All.SendAsync("ErrorNotification", jsonEnvironment);

                                        if(startTime == null)
                                        {
                                            return;
                                        }
                                        if ((DateTime.UtcNow.AddHours(7) - startTime.Value).TotalMinutes < 2 * count)
                                        {
                                            await _mqttClient.Publish($"SHRIMP_POND/START", "START", false);
                                            await _mqttClient.Publish($"SHRIMP_POND/START_TIME/STATUS", "START", false);

                                            await SendMail("vu34304@gmail.com", "Đã gửi lại danh sách ao đo", "");
                                        }
                                    }

                                    break;
                                }
                            case "RS485":
                                {
                                    if (payloadMessage == "BAD")
                                    {
                                        //var gmail = new GmailMessage
                                        //{
                                        //    To = "vu34304@gmail.com",
                                        //    Subject = "Lỗi RS485",
                                        //    Body = "Vui lòng kiểm tra lại !"

                                        //};

                                        //await _gmailSender.SendGmail(gmail);

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
                                        //var gmail = new GmailMessage
                                        //{
                                        //    To = "vu34304@gmail.com",
                                        //    Subject = "Lỗi PCF8574",
                                        //    Body = "Vui lòng kiểm tra lại !"

                                        //};

                                        //await _gmailSender.SendGmail(gmail);

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
                                        //var gmail = new GmailMessage
                                        //{
                                        //    To = "vu34304@gmail.com",
                                        //    Subject = "Lỗi ADC",
                                        //    Body = "Vui lòng kiểm tra lại !"

                                        //};

                                        //await _gmailSender.SendGmail(gmail);

                                        //Gui signalR
                                        var error = new ErrorNotification(topic2, payloadMessage);

                                        string jsonEnvironment = JsonConvert.SerializeObject(error);

                                        await _hubContext.Clients.All.SendAsync("ErrorNotification", jsonEnvironment);
                                    }

                                    break;
                                }

                            case "EMERGENCY_STOP":
                                {
                                    if (payloadMessage == "TRUE")
                                    {

                                        //await _mqttClient.Publish($"SHRIMP_POND/SELECT_POND", "", false);


                                        //var gmail = new GmailMessage
                                        //{
                                        //    To = "vu34304@gmail.com",
                                        //    Subject = "Lỗi nghiêm trọng",
                                        //    Body = "Đã dừng hệ thống!"

                                        //};

                                        //await _gmailSender.SendGmail(gmail);

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

        private async Task SendMail(string email, string subject, string body)
        {
            var gmail = new GmailMessage
            {
                To = email,
                Subject = subject,
                Body = body
            };

            await _gmailSender.SendGmail(gmail);
        }
    }
}
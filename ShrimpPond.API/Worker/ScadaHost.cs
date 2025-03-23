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

        private readonly IHubContext<NotificationHub> _hubContext;

        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public DateTime? firstTime { get; set; }
        public double unitTime { get; set; }
        public int count { get; set; } = 1;
        public int tempTimer { get; set; }
        public int temp { get; set; } = 1;
        public DateTime time { get; set; }
        public ScadaHost(ManagedMqttClient mqttClient,
            IHubContext<NotificationHub> hubContext,
            IGmailSender gmailSender

        )
        {
            _mqttClient = mqttClient;
            _hubContext = hubContext;

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
                                        //var error = new ErrorNotification(topic2, payloadMessage);

                                        //string jsonEnvironment = JsonConvert.SerializeObject(error);

                                        //await _hubContext.Clients.All.SendAsync("ErrorNotification", jsonEnvironment);

                                    }

                                    if (payloadMessage == "GOOD")
                                    {
                                        //Gui mail
                                        //await SendMail("vu34304@gmail.com", "kết nối mqtt", "đã kết nối mqtt");
                                        //await SendMail("van048483@gmail.com", "kết nối mqttt", "đã kết nối mqtt");

                                        //Gui signalR
                                        //var error = new ErrorNotification(topic2, payloadMessage);

                                        //string jsonEnvironment = JsonConvert.SerializeObject(error);

                                        //await _hubContext.Clients.All.SendAsync("ErrorNotification", jsonEnvironment);

                                        //if(startTime == null)
                                        //{
                                        //    return;
                                        //}
                                        //if ((DateTime.UtcNow.AddHours(7) - startTime.Value).TotalMinutes < 1 * count)
                                        //{
                                        //    await _mqttClient.Publish($"SHRIMP_POND/START", "START", false);
                                        //    await _mqttClient.Publish($"SHRIMP_POND/START_TIME/STATUS", "START", false);

                                        //    await SendMail("vu34304@gmail.com", "Đã gửi lại danh sách ao đo", "");
                                        //}
                                    }

                                    break;
                                }
                            case "RS485":
                                {
                                    if (payloadMessage == "BAD")
                                    {
                                        //Gui signalR
                                        var error = new ErrorNotification(topic2, payloadMessage);

                                        string jsonEnvironment = JsonConvert.SerializeObject(error);

                                        //await _hubContext.Clients.All.SendAsync("ErrorNotification", jsonEnvironment);
                                    }
                                    break;
                                }
                            case "PCF8574":
                                {
                                    if (payloadMessage == "BAD")
                                    {

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
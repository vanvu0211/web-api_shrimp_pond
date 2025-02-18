using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShrimpPond.Application.Contract.GmailService;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Models.Gmail;
using ShrimpPond.Domain.Environments;
using ShrimpPond.Host.MQTTModels;
using ShrimpPond.Infrastructure.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Host.Hosting
{
    public class HostWorker: BackgroundService
    {
        private readonly ManagedMqttClient _mqttClient;
        private readonly IGmailSender _gmailSender;
        private readonly IServiceScopeFactory _scopeFactory;

        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public DateTime? firstTime { get; set; }
        public double unitTime { get; set; }
        public int count { get; set; } = 1;
        public int tempTimer { get; set; }
        public int temp { get; set; } = 1;
        public DateTime time { get; set; }

        public HostWorker(ManagedMqttClient mqttClient, IServiceScopeFactory scopeFactory , IGmailSender gmailSender)
        {
            _mqttClient = mqttClient;
            _scopeFactory = scopeFactory;
            _gmailSender = gmailSender;
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

                        break;
                    }
                case "START_TIME":
                    {
                        switch (payloadMessage)
                        {
                            case "START":
                                {
                                    tempTimer++;
                                    startTime = DateTime.UtcNow.AddHours(7);                                   
                                    break;
                                }

                        }
                        break;
                    }
                case "ENVIRONMENT":
                    {
                        switch (topic2)
                        {
                            case "TEMP":
                                {
                                   
                                    break;
                                }
                            case "VALVE":
                                {
                                   
                                    break;
                                }
                            default:
                                {
                                    if (temp <= count && startTime != null)
                                    {
                                        var unitTime = (DateTime.UtcNow.AddHours(7) - startTime.Value).TotalMinutes * temp / count;
                                        time = startTime.Value.AddMinutes(unitTime);
                                        //await SendMail("vu34304@gmail.com", "unittime ", unitTime.ToString());
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


                                        var test = unitOfWork.environmentStatusRepository.FindAll().Count();

                                        unitOfWork.environmentStatusRepository.Add(data);
                                        await unitOfWork.SaveChangeAsync();


                                        //Gửi dữ liệu

                                        await SendMail("vu34304@gmail.com", "Gửi dữ liệu ao " + topic2, "Dữ liệu: " + JsonConvert.SerializeObject(data));
                                        await SendMail("van048483@gmail.com", "Gửi dữ liệu ao " + topic2, "Dữ liệu: " + JsonConvert.SerializeObject(data));

                                        //await _hubContext.Clients.All.SendAsync("EnvironmentChanged", jsonEnvironment);

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

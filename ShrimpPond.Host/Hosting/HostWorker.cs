using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ShrimpPond.Application.Contract.GmailService;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Models.Gmail;
using ShrimpPond.Domain.Environments;
using ShrimpPond.Host.MQTTModels;
using ShrimpPond.Infrastructure.Communication;
using ShrimpPond.Persistence.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Timer = System.Timers.Timer;


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
        private static Timer timer;

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



            switch (topic1)
            {

                case "POND":
                    {
                        count = int.Parse(payloadMessage);
                        temp = 1;

                        double interval = 5 * 60 * 1000 * count; // 5 phút * count (mili giây)
                        timer = new Timer(interval);
                        timer.Elapsed += async (sender, e) => await TimerElapsed(sender, e); ;
                        timer.AutoReset = false; // Đặt thành true nếu muốn lặp lại
                        timer.Start();
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
                                    if(payloadMessage == "EMPTY")
                                    {
                                        try
                                        {
                                            await SendMail("vu34304@gmail.com", "Gửi dữ liệu ao " + topic2, "Dữ liệu: EMPTY ");
                                            await SendMail("van048483@gmail.com", "Gửi dữ liệu ao " + topic2, "Dữ liệu: EMPTY ");
                                        }
                                        catch { }
                                    }    
                                       
                                    timer.Stop();

                                    using var scope = _scopeFactory.CreateScope();
                                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                                    var farmActive = unitOfWork.timeSettingRepository.FindByCondition(x => x.enableFarm == true).FirstOrDefault();
                                    if(farmActive == null) break;
                                    
                                    var pond = unitOfWork.pondRepository.FindByCondition(x =>x.farmId == farmActive.farmId &&  x.pondName == topic2).FirstOrDefault();
                                    if (pond == null) break;

                                    if (temp <= count && startTime != null)
                                    {
                                            var unitTime = (DateTime.UtcNow.AddHours(7) - startTime.Value).TotalMinutes * temp / count;
                                            time = startTime.Value.AddMinutes(unitTime);// chia thời gian cho mỗi lần đo
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

                                        var valueEnvironment = environment.value;
                                        try
                                        {
                                            valueEnvironment = Math.Round(float.Parse(environment.value), 2).ToString();
                                        }
                                        catch
                                        {
                                            ;
                                        }

                                        var data = new EnvironmentStatus()
                                        {
                                            pondId = pond.pondId,
                                            name = environment.name,
                                            value = valueEnvironment,
                                            timestamp = time,
                                        };

                                        unitOfWork.environmentStatusRepository.Add(data);
                                        await unitOfWork.SaveChangeAsync();
                                        //Gửi dữ liệu

                                        await SendMail("vu34304@gmail.com", "Gửi dữ liệu ao " + topic2, "Dữ liệu: " + JsonConvert.SerializeObject(data));
                                        await SendMail("van048483@gmail.com", "Gửi dữ liệu ao " + topic2, "Dữ liệu: " + JsonConvert.SerializeObject(data));

                                        //So sánh dữ liệu gửi thông báo
                                        switch (environment.name)
                                        {
                                            case "Temperature":
                                                {
                                                    if (float.Parse(environment.value) >= 35 || float.Parse(environment.value) <= 25)
                                                    {
                                                        await SendMail("vu34304@gmail.com", "Cảnh báo nhiệt độ ao " + topic2, "Nhiệt độ: " + environment.value.ToString() + " (độ) ngoài khoảng cho phép");
                                                        await SendMail("van048483@gmail.com", "Cảnh báo nhiệt độ ao " + topic2, "Nhiệt độ: " + environment.value.ToString() + " (độ) ngoài khoảng cho phép");
                                                    }
                                                    break;
                                                }
                                            case "O2":
                                                {
                                                    if (float.Parse(environment.value) >= 7 || float.Parse(environment.value) <= 3)
                                                    {
                                                        await SendMail("vu34304@gmail.com", "Cảnh báo thông số O2 ao " + topic2, "O2: " + environment.value.ToString() + " (mg/l) ngoài khoảng cho phép");
                                                        await SendMail("van048483@gmail.com", "Cảnh báo thông số O2  ao " + topic2, "O2: " + environment.value.ToString() + " (mg/l) ngoài khoảng cho phép");
                                                    }
                                                    break;
                                                }
                                            case "Ph":
                                                {
                                                    if (float.Parse(environment.value) >= 8.7 || float.Parse(environment.value) <= 7.5)
                                                    {
                                                        await SendMail("vu34304@gmail.com", "Cảnh báo độ pH ao " + topic2, "pH: " + environment.value.ToString() + " ngoài khoảng cho phép");
                                                        await SendMail("van048483@gmail.com", "Cảnh báo độ pH ao " + topic2, "pH: " + environment.value.ToString() + " ngoài khoảng cho phép");
                                                    }
                                                    break;
                                                }
                                            default: break;
                                        }
                                    }
                                    break;
                                }
                        }
                        break;
                    }          
            }
        }

        private async Task TimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            await _mqttClient.Publish($"SHRIMP_POND/START", "START", false);
            await _mqttClient.Publish($"SHRIMP_POND/START_TIME/STATUS", "START", false);

            await SendMail("vu34304@gmail.com", "Gửi lại   ", "");
            await SendMail("van048483@gmail.com", "Gửi lại  ", "" );
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

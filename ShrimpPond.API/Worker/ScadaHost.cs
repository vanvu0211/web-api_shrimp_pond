using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using ShrimpPond.API.Hubs;
using ShrimpPond.API.MQTTModels;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Infrastructure.Communication;
using ShrimpPond.Domain.Environments;


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
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
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
            payloadMessage = payloadMessage.Replace("true", "\"TRUE\"");
            payloadMessage = payloadMessage.Replace("[", "");
            payloadMessage = payloadMessage.Replace("]", "");

            //// gửi chỉ số oee, xử lí lưu database, gửi lên web
            switch (topic2)
            {
                case "ENVIRONMENT":
                {
                    var environment = JsonConvert.DeserializeObject<TempleteObject>(payloadMessage);
                    var environmentSend = new EnviromentSend
                    {
                        PondId = topic1,
                        name = environment.name,
                        timestamp = environment.timestamp,
                        value = environment.value,
                    };

                    string jsonEnvironment = JsonConvert.SerializeObject(environmentSend);
                    // moi truong/ ten cam bien/ value
                    //var envirBuffer = new TagChangedNotification(topicSegments[1], topicSegments[3], jsonEnvironment);
                    //_buffer.Update(envirBuffer);
                    await _hubContext.Clients.All.SendAsync("EnvironmentChanged", jsonEnvironment);

                    //Lưu vào database
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                        var data = new EnvironmentStatus()
                        {
                            PondId = topic1,
                            Name = environment.name,
                            Value = environment.value,
                            Timestamp = DateTime.Parse(environment.timestamp),
                        };

                        var pond = _unitOfWork.pondRepository.FindAll().Where(x => x.PondId == data.PondId).ToList();
                        if (pond.Count() != 0)
                        {
                            _unitOfWork.environmentStatusRepository.Add(data);
                            await _unitOfWork.SaveChangeAsync();
                        }

                        break;
                    }
                }
            }
        }
    }
}
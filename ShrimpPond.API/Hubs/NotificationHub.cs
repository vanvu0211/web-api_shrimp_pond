using Microsoft.AspNetCore.SignalR;
using ShrimpPond.API.Worker;
using ShrimpPond.API.Hubs;
using ShrimpPond.Infrastructure.Communication;
using Buffer = ShrimpPond.API.Worker.Buffer;
using ShrimpPond.API.MQTTModels;
using Newtonsoft.Json;



namespace ShrimpPond.API.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly Buffer _buffer;
        private readonly ManagedMqttClient _mqttClient;

        public NotificationHub(Buffer buffer, ManagedMqttClient mqttClient)
        {
            _buffer = buffer;
            _mqttClient = mqttClient;
        }

        public string SendAllMachineOee()
        {
            //var a = new List<OeeSend>();
            //List<TagChangedNotification> Oee = _buffer.GetMachineOee();
            //foreach (TagChangedNotification oee in Oee)
            //{
            //    var oeeObjectFromBuffer = JsonConvert.DeserializeObject<OEE>(oee.TagValue.ToString());
            //    var b = new OeeSend
            //    {
            //        Topic = oee.Topic,
            //        DeviceId = oee.DeviceId,
            //        IdleTime = oeeObjectFromBuffer.IdleTime,
            //        ShiftTime = oeeObjectFromBuffer.ShiftTime,
            //        Oee = oeeObjectFromBuffer.Oee,
            //        OperationTime = oeeObjectFromBuffer.OperationTime,
            //        Timestamp = oeeObjectFromBuffer.TimeStamp,
            //    };
            //    a.Add(b);
            //}
            //string jsonDb = JsonConvert.SerializeObject(a);
            return "";
        }

        public string SendAllEnvironment()
        {
            var envSend = new List<EnviromentSend>();
            List<TagChangedNotification> envs = _buffer.GetEnvironment();
            foreach (TagChangedNotification env in envs)
            {
                var envObjectFromBuffer = JsonConvert.DeserializeObject<EnviromentSend>(env.TagValue.ToString());

                envSend.Add(envObjectFromBuffer);
            }
            string jsonDb = JsonConvert.SerializeObject(envSend);
            return jsonDb;
            return "";

        }


        public string SendAll()
        {
            string allTags = _buffer.GetAllTag();
            return allTags;
        }

        public async Task SendAllTag()
        {
            //string allTags = _buffer.GetAllTag();

            //await Clients.All.SendAsync("GetAll", allTags);
        }

        public async Task SendCommand(string deviceId, string command)
        {
            string topic = $"IOT/Detail/NewDetailToWork/{deviceId}";

            await _mqttClient.Publish(topic, command, true);
        }
    }
}

using Microsoft.Extensions.Caching.Memory;
using Quartz;
using ShrimpPond.Application.Contract.GmailService;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Models.Gmail;
using ShrimpPond.Infrastructure.Communication;

namespace ShrimpPond.API.Worker
{
    [DisallowConcurrentExecution]
    public class CheckTimeSettingHost : IJob
    {
        private readonly ManagedMqttClient _mqttClient;
        private readonly IGmailSender _gmailSender;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMemoryCache _memoryCache;
        // Biến trạng thái để lưu thời gian gửi gần nhất
        private DateTime? lastSentTime = null;

        public CheckTimeSettingHost(ManagedMqttClient mqttClient, IGmailSender gmailSender, IServiceScopeFactory scopeFactory, IMemoryCache memoryCache)
        {
            _mqttClient = mqttClient;
            _gmailSender = gmailSender;
            _scopeFactory = scopeFactory;
            _memoryCache = memoryCache;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using var scope = _scopeFactory.CreateScope();

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            //Lay danh sach ao da kich hoat
            var ponds = unitOfWork.pondRepository
                        .FindAll()
                        .Where(x => x.status == Domain.PondData.EPondStatus.Active);

            var dataPonds = string.Join(",", ponds.Select(pond => pond.pondId));

            // Lấy TimeSettingId lớn nhất
            var timeSettingId = unitOfWork.timeSettingRepository.FindAll().Count();
            if (timeSettingId != 0)
            {
                // Lấy danh sách thời gian từ timeSettingObjects
                var timeSettingTimes = unitOfWork.timeSettingObjectRepository.FindAll()
                    .Where(x => x.timeSettingId == timeSettingId)
                    .Select(x => Convert.ToDateTime(x.time))
                    .ToList();

                foreach (var data in timeSettingTimes)
                {
                    Console.WriteLine(data);
                }

                // Thời gian hiện tại
                var nowTime = DateTime.UtcNow.AddHours(7);
                Console.WriteLine(nowTime.ToString());
                // Kiểm tra nếu bất kỳ thời gian nào nằm trong khoảng ±1 phút
                var timeMatch = timeSettingTimes.Any(time => (time.Hour == nowTime.Hour && time.Minute <= nowTime.Minute + 1 && time.Minute >= nowTime.Minute - 1)
                   );

                Console.WriteLine(timeMatch.ToString());
                // Kiểm tra điều kiện gửi lệnh MQTT
                if (timeMatch && (lastSentTime == null || (nowTime - lastSentTime.Value).TotalSeconds > 60))
                {
                    lastSentTime = nowTime;
                    Console.Write(lastSentTime);
                    await _mqttClient.Publish($"SHRIMP_POND/SELECT_POND", dataPonds, true);
                    await _mqttClient.Publish($"SHRIMP_POND/POND/COUNT", ponds.Count().ToString(), true);
                    await Task.Delay(1000);
                    await _mqttClient.Publish($"SHRIMP_POND/START", "START", false);
                    await _mqttClient.Publish($"SHRIMP_POND/START_TIME/STATUS", "START", false);

                    //await SendMail("vu34304@gmail.com", "Gửi danh sách Ao đo thành công", "");
                    //await SendMail("van048483@gmail.com", "Gửi danh sách ao đo thành công", "");

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

using Microsoft.AspNetCore.SignalR;
using ShrimpPond.API.Hubs;
using ShrimpPond.Application.Contract.GmailService;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Models.Gmail;
using ShrimpPond.Infrastructure.Communication;
using ShrimpPond.Persistence.Migrations;
using Timer = System.Timers.Timer;

namespace ShrimpPond.API.Worker
{
    public class CheckTimeSettingHost: BackgroundService
    {
        private readonly ManagedMqttClient _mqttClient;
        private readonly IGmailSender _gmailSender;
        private readonly IServiceScopeFactory _scopeFactory;
        // Biến trạng thái để lưu thời gian gửi gần nhất
        private DateTime? lastSentTime = null;

        public CheckTimeSettingHost(ManagedMqttClient mqttClient, IGmailSender gmailSender, IServiceScopeFactory scopeFactory)
        {
            _mqttClient = mqttClient;
            _gmailSender = gmailSender;
            _scopeFactory = scopeFactory;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while( ! stoppingToken.IsCancellationRequested )
            {
                await CheckTime();
                await Task.Delay(1000 * 30, stoppingToken);
            }
        }

        private async Task CheckTime()
        {
            using var scope = _scopeFactory.CreateScope();

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            //Lay danh sach ao da kich hoat
            var ponds = unitOfWork.pondRepository
                        .FindAll()
                        .Where(x => x.Status == Domain.PondData.EPondStatus.Active);

            var dataPonds = string.Join(",", ponds.Select(pond => pond.PondId));

            // Lấy TimeSettingId lớn nhất
            var timeSettingId = unitOfWork.timeSettingRepository.FindAll().Count();
            if(timeSettingId != 0)
            {
                // Lấy danh sách thời gian từ timeSettingObjects
                var timeSettingTimes = unitOfWork.timeSettingObjectRepository.FindAll()
                    .Where(x => x.TimeSettingId == timeSettingId)
                    .Select(x => Convert.ToDateTime(x.Time))
                    .ToList();

                // Thời gian hiện tại
                var nowTime = DateTime.UtcNow.AddHours(7);

                // Kiểm tra nếu bất kỳ thời gian nào nằm trong khoảng ±1 phút
                var timeMatch = timeSettingTimes.Any(time =>
                    Math.Abs((time - nowTime).TotalMinutes) <= 1);

                // Kiểm tra điều kiện gửi lệnh MQTT
                if (timeMatch && (lastSentTime == null || (nowTime - lastSentTime.Value).TotalSeconds > 50))
                {
                    lastSentTime = nowTime;

                    await _mqttClient.Publish($"SHRIMP_POND/SELECT_POND", dataPonds, false);
                    await Task.Delay(1000);
                    await _mqttClient.Publish($"SHRIMP_POND/START", "START", false);
                    await _mqttClient.Publish($"SHRIMP_POND/START_TIME/START_TIME", "START", false);

                    await SendMail("vu34304@gmail.com", "Gửi thời gian cài đặt thành công vào lúc: " + nowTime.ToLongTimeString(), "Danh sách ao đo:" + dataPonds);
                    await SendMail("van048483@gmail.com", "Gửi thời gian cài đặt thành công vào lúc: " + nowTime.ToLongTimeString(), "Danh sách ao đo:" + dataPonds);
                    // Cập nhật thời gian gửi gần nhất
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

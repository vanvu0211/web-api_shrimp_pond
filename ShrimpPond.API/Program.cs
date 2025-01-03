using EquipmentManagement.Infrastructure;
using ShrimpPond.API.Hubs;
using ShrimpPond.API.Middleware;
using ShrimpPond.API.Worker;
using ShrimpPond.Application;
using ShrimpPond.Infrastructure.Communication;
using ShrimpPond.Persistence;
using Buffer = ShrimpPond.API.Worker.Buffer;
using Quartz;

namespace ShrimpPond.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Add services to the container.

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                           builder =>
                           {
                               builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                           });
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<MqttOptions>(builder.Configuration.GetSection("MqttOptions"));
            builder.Services.AddSignalR();
            builder.Services.AddSingleton<ManagedMqttClient>();
            builder.Services.AddSingleton<Buffer>();
            builder.Services.AddHostedService<ScadaHost>();
            builder.Services.AddQuartz(options =>
            {
                var jobKey = JobKey.Create("Logging Job");

                options.AddJob<CheckTimeSettingHost>(jobKey)
                        .AddTrigger(trigger =>
                        {
                            trigger.ForJob(jobKey)
                                    .WithSimpleSchedule(schedule => schedule.WithIntervalInMinutes(1).RepeatForever());
                        });
            });
            builder.Services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });


            builder.Services.AddControllers();
            builder.Services.AddMemoryCache();
            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors("AllowAll");

            app.MapControllers();
            app.MapHub<NotificationHub>("/notificationHub");

            app.Run();

        }
    }
}


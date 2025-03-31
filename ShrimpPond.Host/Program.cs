using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using ShrimpPond.Application;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Host.Hosting;
using ShrimpPond.Host.Hubs; // Namespace chứa Hub
using ShrimpPond.Infrastructure;
using ShrimpPond.Infrastructure.Communication;
using ShrimpPond.Persistence;
using ShrimpPond.Persistence.Repository.Generic;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseUrls("http://103.170.122.142:5000"); // Lắng nghe trên IP và port cụ thể
        webBuilder.ConfigureServices((context, services) =>
        {
            // Đăng ký các dịch vụ
            services.AddApplicationServices();
            services.AddInfrastructureServices(context.Configuration);
            services.AddPersistenceServices(context.Configuration);
            services.AddHostedService<HostWorker>();
            services.AddSignalR(); // Thêm SignalR

            // Cấu hình Quartz
            services.AddQuartz(options =>
            {
                var jobKey = JobKey.Create("Logging Job");
                options.AddJob<HostSchedule>(jobKey)
                       .AddTrigger(trigger =>
                       {
                           trigger.ForJob(jobKey)
                                  .WithSimpleSchedule(schedule => schedule.WithIntervalInMinutes(1).RepeatForever());
                       });
            });
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            // Cấu hình MQTT
            services.Configure<MqttOptions>(context.Configuration.GetSection("MqttOptions"));
            services.AddSingleton<ManagedMqttClient>();

            // Đăng ký UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Cấu hình Windows Service
            services.AddWindowsService(options =>
            {
                options.ServiceName = "Scada Host";
            });
        })
        .Configure(app =>
        {
            app.UseRouting();

            // Thêm middleware cho SignalR
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MachineHub>("/machineHub"); // Định nghĩa endpoint cho SignalR Hub
            });
        });
    });

var host = builder.Build();
await host.RunAsync();
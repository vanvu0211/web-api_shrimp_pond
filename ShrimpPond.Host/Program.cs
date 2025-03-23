using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using ShrimpPond.API.Worker;
using ShrimpPond.Application;
using ShrimpPond.Application.Contract.Persistence;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Contract.SmsService;
using ShrimpPond.Host.Hosting;
using ShrimpPond.Infrastructure;
using ShrimpPond.Infrastructure.SmsService;
using ShrimpPond.Persistence;
using ShrimpPond.Persistence.Repository;
using ShrimpPond.Persistence.Repository.Generic;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) =>
    {
        services.AddApplicationServices();
        services.AddInfrastructureServices(builder.Configuration);
        services.AddPersistenceServices(builder.Configuration);
        services.AddHostedService<HostWorker>();
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
        services.Configure<ShrimpPond.Infrastructure.Communication.MqttOptions>(builder.Configuration.GetSection("MqttOptions"));
        services.AddSingleton<ShrimpPond.Infrastructure.Communication.ManagedMqttClient>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        


        services.AddWindowsService(options =>
        {
            options.ServiceName = "Scada Host";
        });
    })
    .Build();

await host.RunAsync();

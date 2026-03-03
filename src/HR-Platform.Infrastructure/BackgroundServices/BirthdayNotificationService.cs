using ErrorOr;
using HR_Platform.Application.Notifications.SendNotificationsByDate;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HR_Platform.Infrastructure.BackgroundServices;

public class BirthdayNotificationService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public BirthdayNotificationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        DateTime scheduledTime = DateTime.Today.AddDays(1).AddHours(5); // 5 am - 12 am

        if (DateTime.Now > scheduledTime)
        {
            scheduledTime = scheduledTime.AddDays(1); // Next Day
        }

        while (!stoppingToken.IsCancellationRequested)
        {            
            TimeSpan delay = scheduledTime - DateTime.Now;

            if (delay.TotalMilliseconds > 0)
            {
                await Task.Delay(delay, stoppingToken); // Start in the scheduled time
            }

            // Excecute the task
            try
            {
                await RunDailyBirthdayNotificationTaskAsync(scheduledTime, stoppingToken);

                Console.WriteLine($"Birthday notifications sended: {DateTime.Now}");
            }
            catch (Exception)
            {

            }

            // Adjust hour to the next day
            scheduledTime = scheduledTime.AddDays(1);
        }
    }

    private async Task RunDailyBirthdayNotificationTaskAsync(DateTime scheduledTime, CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            // Usar mediator o cualquier servicio scoped
            await mediator.Send(new SendNotificationsByDateQuery(scheduledTime), stoppingToken);
        }

        Console.WriteLine($"Daily Task excecute to: {DateTime.Now}");
    }
}

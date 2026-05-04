// using System.ComponentModel;

// public class BackgroundScopeConsumer : BackgroundService
// {
//     public IServiceProvider _services;
//     public BackgroundScopeConsumer(IServiceProvider services)
//     {
//         _services = services;
//     }

//     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         await DoWork(stoppingToken);
//     }
    
//     private async Task DoWork(CancellationToken stoppingToken)
//     {
//         using (var scope = _services.CreateScope())
//         {
//             var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IntegracaoTray>();
//             await scopedProcessingService.GetOrders();
//         }
//     }

// }
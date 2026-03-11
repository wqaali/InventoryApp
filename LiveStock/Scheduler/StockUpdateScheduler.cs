using LiveStockBL.Hubs;
using LiveStockBL.ServiceContract;
using LiveStockBL.Services;
using Microsoft.AspNetCore.SignalR;

namespace LiveStock.Scheduler
{
    public class StockUpdateScheduler:BackgroundService
    {
        private readonly IHubContext<StockHub> _hubContext;
        private readonly IStockService _stockService;

        public StockUpdateScheduler(
            IHubContext<StockHub> hubContext,
            IStockService stockService)
        {
            _hubContext = hubContext;
            _stockService = stockService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var stocks = _stockService.UpdatePrices();

                await _hubContext.Clients.All.SendAsync("ReceiveStocks", stocks);

                await Task.Delay(5000);
            }
        }
    }
}

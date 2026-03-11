using LiveStockBL.DTO;
using LiveStockBL.ServiceContract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveStockBL.Services
{
    public class StockService : IStockService
    {
        private readonly List<Stock> _stocks = new()
    {
        new Stock{ Id=1, Symbol="AAPL", Price=180,Quantity=2 },
        new Stock{ Id=2, Symbol="MSFT", Price=320,Quantity=5 },
        new Stock{ Id=3, Symbol="GOOG", Price=140,Quantity=10 },
        new Stock{ Id=4, Symbol="TSLA", Price=250,Quantity=6 }
    };
        public List<Stock> GetStocks()
        {
            return _stocks;
        }

        public List<Stock> UpdatePrices()
        {
            var rand = new Random();

            foreach (var stock in _stocks)
            {
                // Generate a random change between -5 and +5
                stock.Quantity = Math.Clamp(stock.Quantity + rand.Next(1, 11), 0, 200);
            }

            return _stocks;
        }
    }
}

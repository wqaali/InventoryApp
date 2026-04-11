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
        new Stock{ Id=1, Symbol="LG Laptop 548", Price1=180,Quantity=2 },
        new Stock{ Id=2, Symbol="Samsung Wath 6", Price1=320,Quantity=5 },
        new Stock{ Id=3, Symbol="Sony XM500", Price1=140,Quantity=10 },
        new Stock{ Id=4, Symbol="XYZ", Price1=250,Quantity=6 }
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
                // Generate a random change between 0 and 300
                stock.Quantity = rand.Next(0, 201);
            }

            return _stocks;
        }
    }
}

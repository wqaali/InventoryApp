using LiveStockBL.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveStockBL.ServiceContract
{
    public interface IStockService
    {
        List<Stock> GetStocks();
        List<Stock> UpdatePrices();
    }
}

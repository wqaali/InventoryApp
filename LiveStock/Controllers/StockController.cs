using LiveStockBL.ServiceContract;
using Microsoft.AspNetCore.Mvc;

namespace LiveStock.Controllers
{
    [ApiController]
    [Route("api/stocks")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }
        [HttpGet]
        public IActionResult GetStocks()
        {
            return Ok(_stockService.GetStocks());
        }
    }
}

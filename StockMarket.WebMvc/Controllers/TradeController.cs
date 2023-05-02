using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StockMarket.Core.DTO;
using StockMarket.Core.ServiceInterfaces;
using StockMarket.WebMvc.Config;

namespace StockMarket.WebMvc.Controllers
{
    [Route("[controller]")]
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly IOptions<TradingOptions> _options;
        private readonly IConfiguration _configuration;

        public TradeController(IFinnhubService finnhubService, IOptions<TradingOptions> options, IConfiguration configuration)
        {
            _finnhubService = finnhubService;
            _options = options;
            _configuration = configuration;
        }

        [Route("/")]
        [Route("[action]")]
        [Route("~/[controller]")]
        public async Task<IActionResult> Index()
        {
            CompanyProfile companyProfile = await _finnhubService.GetCompanyProfile(_options.Value.DefaultStockSymbol);
            StockPriceQuote stockPriceQuote = await _finnhubService.GetStockPriceQuote(_options.Value.DefaultStockSymbol);

            StockTrade stockTrade = new()
            {
                StockSymbol = companyProfile.Ticker,
                StockName = companyProfile.Name,
                Price = stockPriceQuote.CurrentPrice,
            };

            ViewBag.FinnhubToken = _configuration["x-finnhub-token"];

            return View(stockTrade);
        }
        

    }
}
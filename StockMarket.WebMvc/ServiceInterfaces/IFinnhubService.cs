using StockMarket.WebMvc.Models;

namespace StockMarket.WebMvc.ServiceInterfaces
{
    public interface IFinnhubService
    {
        Task<CompanyProfile> GetCompanyProfile(string stockSymbol);

        Task<StockPriceQuote> GetStockPriceQuote(string stockSymbol);
    }
}

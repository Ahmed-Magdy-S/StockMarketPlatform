using StockMarket.Models;

namespace StockMarket.ServiceInterfaces
{
    public interface IFinnhubService
    {
        Task<CompanyProfile> GetCompanyProfile(string stockSymbol);

        Task<StockPriceQuote> GetStockPriceQuote(string stockSymbol);
    }
}

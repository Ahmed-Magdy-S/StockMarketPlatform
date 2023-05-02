
using StockMarket.Core.DTO;

namespace StockMarket.Core.ServiceInterfaces
{
    public interface IFinnhubService
    {
        Task<CompanyProfile> GetCompanyProfile(string stockSymbol);

        Task<StockPriceQuote> GetStockPriceQuote(string stockSymbol);
    }
}

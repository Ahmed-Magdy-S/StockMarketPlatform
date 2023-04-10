using StockMarket.Core.DTO;
using StockMarket.ServiceInterfaces;

namespace StockMarket.Services
{
    public class StockService : IStockService
    {
        public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null) throw new ArgumentNullException(nameof(buyOrderRequest), "The buyOrderRequest is null ");

            if (buyOrderRequest.Quantity < 1 ||
                buyOrderRequest.Quantity > 100000 ||
                buyOrderRequest.Price < 1 ||
                buyOrderRequest.Price > 10000 ||
                buyOrderRequest.StockSymbol == null ||
                buyOrderRequest.DateAndTimeOfOrder < new DateTime(2000,1,1)
                )
            {
                throw new ArgumentException("Some arguments are not valid");
            }

            BuyOrderResponse buyOrderResponse = new BuyOrderResponse() 
            {
                StockName = "M",
                StockSymbol = buyOrderRequest.StockSymbol,
            };

            return Task.FromResult(buyOrderResponse);

        }

        public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            throw new NotImplementedException();
        }

        public Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            throw new NotImplementedException();
        }

        public Task<List<SellOrderResponse>> GetSellOrders()
        {
            throw new NotImplementedException();
        }
    }
}

using StockMarket.Core.Entities;
using StockMarket.Infrastructure.DTO;
using StockMarket.Infrastructure.DTO.Infrastructure.Utils;
using StockMarket.WebMvc.ServiceInterfaces;

namespace StockMarket.WebMvc.Services
{
    public class StockService : IStockService
    {
        //Acts as fake databases
        private List<BuyOrder> _buyOrders = new();
        private List<SellOrder> _sellOrders = new();
        public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            //The argument cannot be null
            if (buyOrderRequest == null) throw new ArgumentNullException(nameof(buyOrderRequest), "The buyOrderRequest is null ");

            //Model Validation
            ModelValidation<BuyOrderRequest>.Validate(buyOrderRequest);

            BuyOrderResponse buyOrderResponse = new () 
            {
                StockName = "M",
                StockSymbol = buyOrderRequest.StockSymbol,
                BuyOrderID = Guid.NewGuid()
            };
            
            _buyOrders.Add(buyOrderRequest.ToBuyOrder());

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

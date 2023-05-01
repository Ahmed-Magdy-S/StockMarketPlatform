using StockMarket.Core.Entities;
using StockMarket.Infrastructure.DTO;
using StockMarket.Infrastructure.DTO.Infrastructure.Utils;
using StockMarket.WebMvc.ServiceInterfaces;

namespace StockMarket.WebMvc.Services
{
    public class StockService : IStockService
    {
        //Acts as fake databases
        private readonly List<BuyOrder> _buyOrders = new();
        private readonly List<SellOrder> _sellOrders = new();
        public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            //Validation
            ModelValidation<BuyOrderRequest>.Validate(buyOrderRequest);

            //convert request to real entity
            BuyOrder? buyOrder = buyOrderRequest?.ToBuyOrder();

            //convert the real enity into response object for sending to the user.
            var buyOrderResponse = buyOrder?.ToBuyOrderResponse();

            //Act as database
            _buyOrders.Add(buyOrder!);

            //Sending response object to the user
            return Task.FromResult(buyOrderResponse!);
        }

        public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            //validation
            ModelValidation<SellOrderRequest>.Validate(sellOrderRequest);
            
            var sellOrder = sellOrderRequest?.ToSellOrder();

            var sellOrderResponse = sellOrder?.ToSellOrderResponse();

            _sellOrders.Add(sellOrder!);

            return Task.FromResult(sellOrderResponse!);
        }

        public Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrderResponse> buyOrdersResponseList = new();

            foreach(var buyOrder in _buyOrders)
            {
                buyOrdersResponseList.Add(buyOrder.ToBuyOrderResponse());
            }

            return Task.FromResult(buyOrdersResponseList);
        }

        public Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrderResponse> sellOrderResponses = new();

            foreach (var sellOrder in _sellOrders)
            {
                sellOrderResponses.Add(sellOrder.ToSellOrderResponse());
            }

            return Task.FromResult(sellOrderResponses);

        }
    }
}

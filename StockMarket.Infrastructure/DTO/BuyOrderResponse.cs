using StockMarket.Core.Entities;

namespace StockMarket.Infrastructure.DTO
{
    public class BuyOrderResponse
    {
        public Guid BuyOrderID { get; set; }
        public required string StockName { get; set; }
        public required string StockSymbol { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
        public double TradeAmount { get; set; }
    }

    public static class ConvertToResponse
    {
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse
            {
                BuyOrderID = buyOrder.Id,
                StockName = buyOrder.StockName,
                StockSymbol = buyOrder.StockSymbol,
                Price = buyOrder.Price,
                Quantity = buyOrder.Quantity,
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
                TradeAmount = buyOrder.Price * buyOrder.Quantity
            };
        }
    }
}

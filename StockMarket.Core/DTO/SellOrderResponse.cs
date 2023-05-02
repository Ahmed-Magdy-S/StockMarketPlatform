using StockMarket.Core.Entities;

namespace StockMarket.Core.DTO
{
    public class SellOrderResponse
    {
        public Guid SellOrderID { get; set; }
        public required string StockName { get; set; }
        public required string StockSymbol { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }
        public double TradeAmount { get; set; }
    }

    public static class ConvertToSellOrderResponse
    {
        public static SellOrderResponse ToSellOrderResponse (this SellOrder response)
        {
            return new SellOrderResponse
            {
                StockName = response.StockName,
                StockSymbol = response.StockSymbol,
                Price = response.Price,
                TradeAmount = response.Price * response.Quantity,
                Quantity = response.Quantity,
                SellOrderID = response.Id,
                DateAndTimeOfOrder = response.DateAndTimeOfOrder
            };
        }
    }
}

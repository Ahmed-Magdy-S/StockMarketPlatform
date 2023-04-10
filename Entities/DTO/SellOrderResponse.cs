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
}

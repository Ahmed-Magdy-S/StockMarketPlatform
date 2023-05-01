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
}

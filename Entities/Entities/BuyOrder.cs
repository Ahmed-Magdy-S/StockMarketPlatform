namespace StockMarket.Core.Entities
{
    public class BuyOrder
    {
        public Guid Id { get; set; }
        public required string StockName { get; set; }
        public required string StockSymbol { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }
        public uint Quantity { get; set; }
        public double Price { get; set; }

    }
}

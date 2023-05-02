using System.ComponentModel.DataAnnotations;
using StockMarket.Core.Entities;
using StockMarket.Infrastructure.Utils;

namespace StockMarket.Core.DTO
{
    public class SellOrderRequest
    {
        [Required]
        public required string StockSymbol { get; set; }

        [Required]
        public required string StockName { get; set; }

        [MinDate("01/01/2000")]
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000)]
        public uint Quantity { get; set; }

        [Range(1.0, 10000.0)]
        public double Price { get; set; }

        public SellOrder ToSellOrder()
        {
            return new SellOrder
            {
                Id = Guid.NewGuid(),
                Price = Price,
                StockName = StockName,
                StockSymbol = StockSymbol,
                DateAndTimeOfOrder  = DateAndTimeOfOrder,
                Quantity = Quantity
            };
        }
    }
}

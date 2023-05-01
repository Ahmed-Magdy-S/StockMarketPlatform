﻿using System.ComponentModel.DataAnnotations;
using StockMarket.Infrastructure.Utils;

namespace StockMarket.Infrastructure.DTO
{
    public class SellOrderRequest
    {
        [Required]
        public string? StockSymbol { get; set; }

        [Required]
        public string? StockName { get; set; }

        [MinDate("01/01/2000")]
        public DateTime? DateAndTimeOfOrder { get; set; }

        [Range(1, 100000)]
        public uint Quantity { get; set; }

        [Range(1.0, 10000.0)]
        public double Price { get; set; }
    }
}

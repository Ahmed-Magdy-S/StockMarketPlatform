using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace StockMarket.Infrastructure.Utils;

public class MinDateAttribute : ValidationAttribute
{
    private DateTime MinDate { get; }
    
    public MinDateAttribute(string minDate)
    {
        MinDate = DateTime.Parse(minDate);
    }
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult($"Error, you need to provide a datetime");
        }

        var providedDate = (DateTime)value;

        return providedDate < MinDate ? new ValidationResult($"Error, The provided date {providedDate.ToString(CultureInfo.InvariantCulture)} shouldn't be older than {MinDate.ToString(CultureInfo.InvariantCulture)}") : ValidationResult.Success;
    }
    
}
using System.ComponentModel.DataAnnotations;

namespace StockMarket.Infrastructure.DTO.Infrastructure.Utils;

public static class ModelValidation<T>
{
    public static void Validate(T? obj)
    {
        //The argument cannot be null
        if (obj == null) throw new ArgumentNullException(nameof(obj.GetType), "The instance is null");


        var validationContext = new ValidationContext(obj);

        var validationResults =  new List<ValidationResult>();
        
        var isValid = Validator.TryValidateObject(obj, validationContext, validationResults ,true);

       if (!isValid) throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
    }
    
}
namespace RadCars.Web.Infrastructure.ModelBinders;

using System.Globalization;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.ModelBinding;

public class CustomDateTimeFormatModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

        var dateStr = valueProviderResult.FirstValue;
        if (string.IsNullOrEmpty(dateStr))
        {
            return Task.CompletedTask;
        }

        if (DateTime.TryParseExact(dateStr, "dd.MM.yyyy в HH:mm часа", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) == false)
        {
            // If not valid, add a model error
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Invalid date format");
            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(date);
        return Task.CompletedTask;
    }
}
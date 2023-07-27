namespace RadCars.Web.Infrastructure.ModelBinders;

using Microsoft.AspNetCore.Mvc.ModelBinding;

public class CustomDateTimeFormatModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType == typeof(DateTime) ||
            context.Metadata.ModelType == typeof(DateTime?))
        {
            return new CustomDateTimeFormatModelBinder();
        }

        return null!;
    }
}
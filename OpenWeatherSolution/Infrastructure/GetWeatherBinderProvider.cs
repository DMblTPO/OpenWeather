using Microsoft.AspNetCore.Mvc.ModelBinding;
using OpenWeatherSolution.Controllers.Dtos;

namespace OpenWeatherSolution.Infrastructure
{
    public class GetWeatherBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinder _binder = new GetWeatherBinder();

        public IModelBinder GetBinder(ModelBinderProviderContext context)
            => context.Metadata.ModelType == typeof(GetWeatherDto) ? _binder : null;
    }
}
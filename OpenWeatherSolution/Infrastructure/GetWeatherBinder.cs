using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OpenWeatherSolution.Controllers.Dtos;
using OpenWeatherSolution.Extensions;

namespace OpenWeatherSolution.Infrastructure
{
    public class GetWeatherBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var city = bindingContext.ValueProvider.GetValue(nameof(GetWeatherDto.City)).FirstValue;
            var metrics = bindingContext.ValueProvider.GetValue(nameof(GetWeatherDto.Metrics)).FirstValue;
            var lang = bindingContext.ValueProvider.GetValue(nameof(GetWeatherDto.Lang)).FirstValue;
            var rawSortBy = bindingContext.ValueProvider.GetValue(nameof(GetWeatherDto.SortBy)).FirstValue;

            SortParam sortBy = null;
            if (!string.IsNullOrEmpty(rawSortBy))
            {
                var sarr = rawSortBy.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                if (sarr.Length == 1)
                {
                    sortBy = new SortParam {Field = sarr[0], Asc = true};
                }
                else if (sarr.Length == 2)
                {
                    sortBy = new SortParam
                    {
                        Field = sarr[0],
                        Asc = sarr[1].EqualsIgnoreCase("asc")
                    };
                }
            }

            bindingContext.Result = ModelBindingResult.Success(
                new GetWeatherDto
                {
                    City = string.IsNullOrEmpty(city) ? "Kyiv" : city,
                    Metrics = string.IsNullOrEmpty(metrics) ? "Celsius" : metrics,
                    Lang = string.IsNullOrEmpty(lang) ? "En" : lang,
                    SortBy = sortBy
                });
            return Task.CompletedTask;
        }
    }
}
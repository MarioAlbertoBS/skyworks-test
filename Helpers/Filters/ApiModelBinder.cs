using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SkyworksTest.Helpers.Filters;

public class ApiModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        string modelName = bindingContext.ModelType.Name;
        bindingContext.Result = ModelBindingResult.Success(new {});

        return Task.CompletedTask;
    }
}
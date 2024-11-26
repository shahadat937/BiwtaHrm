using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hrm.Api.ModelBindings
{
    public class RequestBodyBinding: IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string? body;

            if(bindingContext.ModelType != typeof(string))
            {
                throw new InvalidOperationException("The model type must be string");
            }

            using(var reader = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                body = await reader.ReadToEndAsync();
            }

            bindingContext.Result = ModelBindingResult.Success(body);

            await Task.CompletedTask;
            return;
        }
    }
}

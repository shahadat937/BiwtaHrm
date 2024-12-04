using System.Collections.Specialized;
using System.Web;
using Hrm.Application.DTOs.Devicecmd;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hrm.Api.ModelBindings
{
    public class DevicecmdBinding: IModelBinder
    {
        public DevicecmdBinding() { }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string? body;

            using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body,System.Text.Encoding.ASCII))
            {
                body = await reader.ReadLineAsync();
            }

            if(body == null)
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Request body should not be empty");
                bindingContext.Result = ModelBindingResult.Success(null);
                return;
            }

            NameValueCollection query = HttpUtility.ParseQueryString(body);

            var Devicecmd = new DeviceCmdResponse();
            Devicecmd.Id = query["ID"];
            Devicecmd.Return = query["Return"];
            Devicecmd.CMD = query["CMD"];

            bindingContext.Result = ModelBindingResult.Success(Devicecmd);
        }
    }
}

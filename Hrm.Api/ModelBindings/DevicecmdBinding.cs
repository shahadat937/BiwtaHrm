using System.Collections.Specialized;
using System.Web;
using Hrm.Application.DTOs.Devicecmd;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Hrm.Api.ModelBindings
{
    public class DevicecmdBinding: IModelBinder
    {
        public DevicecmdBinding() { }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string? body;

            using (var reader = new StreamReader(bindingContext.HttpContext.Request.Body,System.Text.Encoding.ASCII))
            {
                body = await reader.ReadLineAsync();
                string? temp;
                temp = await reader.ReadLineAsync();
                while(temp!=null)
                {
                    string[] keyValuePair = temp.Split("=");
                    if(keyValuePair.Length>1)
                    {
                        parameters.Add(keyValuePair[0], keyValuePair[1]);
                    }
                    temp = await reader.ReadLineAsync();
                }
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

            if(parameters.ContainsKey("~DeviceName"))
            {
                Devicecmd.DeviceName = parameters["~DeviceName"];
            }

            if(parameters.ContainsKey("MAC"))
            {
                Devicecmd.MAC = parameters["MAC"];
            }



            bindingContext.Result = ModelBindingResult.Success(Devicecmd);
        }
    }
}

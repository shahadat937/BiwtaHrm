using Hrm.Application;
using Hrm.Application.Features.Form.Requests.Queries;
namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Form)]
    [ApiController]
    public class FormController:Controller
    {
        private readonly IMediator _mediator;
        
        public FormController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-Form")]
        public async Task<ActionResult> GetForm()
        {
            var command = new GetFormRequest();
            var response = _mediator.Send(command);
            return Ok(response);
        }
    }
}

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
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-FormById/{id}")]
        public async Task<ActionResult> GetFormBy(int id)
        {
            var command = new GetFormByIdRequest { FormId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-SelectedForm")]
        public async Task<ActionResult> GetSelectedForm()
        {
            var command = new GetSelectedFormRequest { };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}

using Hrm.Application;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Holidays)]
    public class HolidaysController
    {
        private readonly IMediator _mediator;

        public HolidaysController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-Holidays")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetHolidays()
        {

        }
    }
}

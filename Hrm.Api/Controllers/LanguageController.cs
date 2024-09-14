using Hrm.Application;
using Hrm.Application.DTOs.Language;
using Hrm.Application.Features.Language.Requests.Commands;
using Hrm.Application.Features.Language.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Shared.Models;
using Hrm.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Language)]
    [ApiController]
    [Authorize]
    public class Language : Controller
    {
        private readonly IMediator _mediator;
        public Language(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-language")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLanguageDto Language)
        {
            var command = new CreateLanguageCommand { LanguageDto = Language };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-language")]
        public async Task<ActionResult> Get()
        {
            var Language = await _mediator.Send(new GetLanguageRequest { });
            return Ok(Language);
        }

        [HttpGet]
        [Route("get-languagebyid/{id}")]
        public async Task<ActionResult<LanguageDto>> Get(int id)
        {
            var Language = await _mediator.Send(new GetLanguageByIdRequest { LanguageId = id });
            return Ok(Language);

        }

        [HttpGet]
        [Route("get-selectedlanguage")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedLanguage()
        {
            var language = await _mediator.Send(new GetSelectedLanguageRequest { });
            return Ok(language);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-language/{id}")]
        public async Task<ActionResult> Put([FromBody] LanguageDto Language)
        {
            var command = new UpdateLanguageCommand { LanguageDto = Language };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-language/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLanguageCommand { LanguageId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}

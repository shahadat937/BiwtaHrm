using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.SiteSettings.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SiteSettings.Handlers.Commands
{
    public class CreateSiteSettingCommandHandler : IRequestHandler<CreateSiteSettingCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSiteSettingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateSiteSettingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var SiteSetting = _mapper.Map<Hrm.Domain.SiteSetting>(request.SiteSettingDto);

            SiteSetting = await _unitOfWork.Repository<Hrm.Domain.SiteSetting>().Add(SiteSetting);
            await _unitOfWork.Save();
            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = SiteSetting.Id;

            return response;
        }
    }
}

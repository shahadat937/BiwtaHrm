using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.SiteSettings.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SiteSettings.Handlers.Commands
{
    public class UpdateSiteSettingCommandHandler : IRequestHandler<UpdateSiteSettingCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSiteSettingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateSiteSettingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var SiteSetting = await _unitOfWork.Repository<Hrm.Domain.SiteSetting>().Get(request.SiteSettingDto.Id);

            if (SiteSetting is null)
                throw new NotFoundException(nameof(SiteSetting), request.SiteSettingDto.Id);

            _mapper.Map(request.SiteSettingDto, SiteSetting);

            await _unitOfWork.Repository<Hrm.Domain.SiteSetting>().Update(SiteSetting);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = SiteSetting.Id;

            return response;
        }
    }
}
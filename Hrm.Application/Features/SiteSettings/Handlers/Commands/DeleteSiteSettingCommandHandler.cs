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
    public class DeleteSiteSettingCommandHandler : IRequestHandler<DeleteSiteSettingCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSiteSettingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteSiteSettingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var SiteSetting = await _unitOfWork.Repository<Hrm.Domain.SiteSetting>().Get(request.Id);

            if (SiteSetting == null)
                throw new NotFoundException(nameof(SiteSetting), request.Id);

            await _unitOfWork.Repository<Hrm.Domain.SiteSetting>().Delete(SiteSetting);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = SiteSetting.Id;

            return response;
        }
    }
}
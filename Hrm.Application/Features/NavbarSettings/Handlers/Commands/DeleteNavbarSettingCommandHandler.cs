using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.NavbarSettings.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.NavbarSettings.Handlers.Commands
{
    public class DeleteNavbarSettingCommandHandler : IRequestHandler<DeleteNavbarSettingCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteNavbarSettingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteNavbarSettingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var NavbarSetting = await _unitOfWork.Repository<Hrm.Domain.NavbarSetting>().Get(request.Id);

            if (NavbarSetting == null)
                throw new NotFoundException(nameof(NavbarSetting), request.Id);

            if (!string.IsNullOrEmpty(NavbarSetting.NavbarLogo))
            {
                var oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images\\TempleteImage", NavbarSetting.NavbarLogo);
                if (File.Exists(oldPhotoPath))
                {
                    File.Delete(oldPhotoPath);
                }
            }

            await _unitOfWork.Repository<Hrm.Domain.NavbarSetting>().Delete(NavbarSetting);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = NavbarSetting.Id;

            return response;
        }
    }
}
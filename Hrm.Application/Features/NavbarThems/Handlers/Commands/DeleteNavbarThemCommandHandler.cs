using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using MediatR;

using Hrm.Application.Features.NavbarThems.Requests.Commands;
using Hrm.Domain;
using Hrm.Application.Responses;

namespace hrm.Application.Features.NavbarThems.Handlers.Commands
{
    public class DeleteNavbarThemCommandHandler : IRequestHandler<DeleteNavbarThemCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteNavbarThemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteNavbarThemCommand request, CancellationToken cancellationToken)
        {

            var response = new BaseCommandResponse();

            var NavbarThem = await _unitOfWork.Repository<NavbarThem>().Get(request.NavbarThemId);

            if (NavbarThem == null)
            {
                response.Success = false;
                response.Message = "Creation Failed";
            }

            await _unitOfWork.Repository<NavbarThem>().Delete(NavbarThem);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Deleted Successfull";


            return response;
        }
    }
}

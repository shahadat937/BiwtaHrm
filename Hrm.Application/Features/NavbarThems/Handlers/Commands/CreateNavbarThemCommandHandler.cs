using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.NavbarThems.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;

namespace Hrm.Application.Features.NavbarThems.Handlers.Commands
{
    public class CreateNavbarThemCommandHandler : IRequestHandler<CreateNavbarThemCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateNavbarThemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateNavbarThemCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var NavbarThem = _mapper.Map<Hrm.Domain.NavbarThem>(request.NavbarThemDto);

            NavbarThem = await _unitOfWork.Repository<Hrm.Domain.NavbarThem>().Add(NavbarThem);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = NavbarThem.Id;

            return response;
        }

    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Commands;
using Hrm.Application.Features.EmpPresentAddresses.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPresentAddresses.Handlers.Commands
{
    public class CreateEmpPresentAddressCommandHandler : IRequestHandler<CreateEmpPresentAddressCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEmpPresentAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpPresentAddressCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpPresentAddress = _mapper.Map<EmpPresentAddress>(request.EmpPresentAddressDto);

            EmpPresentAddress = await _unitOfWork.Repository<EmpPresentAddress>().Add(EmpPresentAddress);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = EmpPresentAddress.Id;

            return response;
        }
    }
}
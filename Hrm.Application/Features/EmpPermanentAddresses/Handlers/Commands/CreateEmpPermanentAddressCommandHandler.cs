using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Commands;
using Hrm.Application.Features.EmpPermanentAddresses.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPermanentAddresses.Handlers.Commands
{
    public class CreateEmpPermanentAddressCommandHandler : IRequestHandler<CreateEmpPermanentAddressCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEmpPermanentAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpPermanentAddressCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpPermanentAddress = _mapper.Map<EmpPermanentAddress>(request.EmpPermanentAddressDto);

            EmpPermanentAddress = await _unitOfWork.Repository<EmpPermanentAddress>().Add(EmpPermanentAddress);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = EmpPermanentAddress.Id;

            return response;
        }
    }
}
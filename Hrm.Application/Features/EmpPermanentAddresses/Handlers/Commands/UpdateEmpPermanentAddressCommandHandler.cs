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
    public class UpdateEmpPermanentAddressCommandHandler : IRequestHandler<UpdateEmpPermanentAddressCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpPermanentAddress> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpPermanentAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpPermanentAddress> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmpPermanentAddressCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpPermanentAddress = await _unitOfWork.Repository<EmpPermanentAddress>().Get(request.EmpPermanentAddressDto.Id);

            _mapper.Map(request.EmpPermanentAddressDto, EmpPermanentAddress);

            await _unitOfWork.Repository<EmpPermanentAddress>().Update(EmpPermanentAddress);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";

            return response;
        }

    }
}

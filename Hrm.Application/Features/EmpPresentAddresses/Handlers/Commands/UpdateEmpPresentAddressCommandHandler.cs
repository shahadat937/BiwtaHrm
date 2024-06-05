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
    public class UpdateEmpPresentAddressCommandHandler : IRequestHandler<UpdateEmpPresentAddressCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpPresentAddress> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpPresentAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpPresentAddress> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmpPresentAddressCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpPresentAddress = await _unitOfWork.Repository<EmpPresentAddress>().Get(request.EmpPresentAddressDto.Id);

            _mapper.Map(request.EmpPresentAddressDto, EmpPresentAddress);

            await _unitOfWork.Repository<EmpPresentAddress>().Update(EmpPresentAddress);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";

            return response;
        }

    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Commands;
using Hrm.Application.Features.EmpJobDetails.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpJobDetails.Handlers.Commands
{
    public class UpdateEmpJobDetailCommandHandler : IRequestHandler<UpdateEmpJobDetailCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpJobDetail> _EmpPersonalInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpJobDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpJobDetail> EmpPersonalInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpPersonalInfoRepository = EmpPersonalInfoRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmpJobDetailCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpJobDetail = await _unitOfWork.Repository<EmpJobDetail>().Get(request.EmpJobDetailDto.Id);

            var empJobDetails = _mapper.Map(request.EmpJobDetailDto, EmpJobDetail);

            await _unitOfWork.Repository<EmpJobDetail>().Update(empJobDetails);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";

            return response;
        }

    }
}

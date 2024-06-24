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
    public class CreateEmpJobDetailCommandHandler : IRequestHandler<CreateEmpJobDetailCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEmpJobDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpJobDetailCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpJobDetail = _mapper.Map<EmpJobDetail>(request.EmpJobDetailDto);

            EmpJobDetail = await _unitOfWork.Repository<EmpJobDetail>().Add(EmpJobDetail);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = EmpJobDetail.Id;

            return response;
        }
    }
}
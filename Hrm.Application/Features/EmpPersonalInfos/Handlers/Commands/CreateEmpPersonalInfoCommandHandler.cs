using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Employee.Requests.Commands;
using Hrm.Application.Features.EmpPersonalInfos.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPersonalInfos.Handlers.Commands
{
    public class CreateEmpPersonalInfoCommandHandler : IRequestHandler<CreateEmpPersonalInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEmpPersonalInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpPersonalInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpPersonalInfo = _mapper.Map<EmpPersonalInfo>(request.EmpPersonalInfoDto);

            EmpPersonalInfo = await _unitOfWork.Repository<EmpPersonalInfo>().Add(EmpPersonalInfo);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = EmpPersonalInfo.Id;

            return response;
        }
    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Employee.Requests.Commands;
using Hrm.Application.Features.EmpBasicInfos.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBasicInfos.Handlers.Commands
{
    public class CreateEmpBasicInfoCommandHandler : IRequestHandler<CreateEmpBasicInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEmpBasicInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpBasicInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpBasicInfo = _mapper.Map<EmpBasicInfo>(request.EmpBasicInfoDto);

            EmpBasicInfo = await _unitOfWork.Repository<EmpBasicInfo>().Add(EmpBasicInfo);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = EmpBasicInfo.Id;

            return response;
        }
    }
}

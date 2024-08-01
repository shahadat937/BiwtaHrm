using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Shift.Validators;
using Hrm.Application.Features.EmpTransferPostings.Requests.Commands;
using Hrm.Application.Features.Shift.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTransferPostings.Handlers.Commands
{
    public class CreateEmpTransferPostingRequestHandler : IRequestHandler<CreateEmpTransferPostingRequest, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEmpTransferPostingRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpTransferPostingRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var empTransferPostings = _mapper.Map<EmpTransferPosting>(request.EmpTransferPostingDto);


            if (request.EmpTransferPostingDto.TransferApproveDate != null || request.EmpTransferPostingDto.IsTransferApprove == false)
            {
                empTransferPostings.TransferApproveStatus = true;
            }

            if ((request.EmpTransferPostingDto.DeptReleaseById != null && request.EmpTransferPostingDto.DeptClearance == true) || request.EmpTransferPostingDto.IsDepartmentApprove == false)
            {
                empTransferPostings.DeptApproveStatus = true;
            }

            if ((request.EmpTransferPostingDto.JoiningStatus == true || request.EmpTransferPostingDto.JoiningDate != null) || request.EmpTransferPostingDto.IsJoining == false)
            {
                empTransferPostings.ApplicationStatus = true;
                empTransferPostings.JoiningStatus = true;
            }

            if (request.EmpTransferPostingDto.TransferApproveStatus == false || request.EmpTransferPostingDto.DeptApproveStatus == false || (request.EmpTransferPostingDto.JoiningStatus == false && request.EmpTransferPostingDto.JoiningDate == null))
            {
                empTransferPostings.ApplicationStatus = false;
            }


            await _unitOfWork.Repository<EmpTransferPosting>().Add(empTransferPostings);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";

            return response;
        }

    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpTransferPostings.Requests.Commands;
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
    public class UpdateEmpTransferPostingStatusCommandHandler : IRequestHandler<UpdateEmpTransferPostingStatusCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpTransferPosting> _EmpTransferPostingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpTransferPostingStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpTransferPosting> EmpTransferPostingRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmpTransferPostingStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpTransferPosting = await _unitOfWork.Repository<EmpTransferPosting>().Get(request.UpdateEmpTransferPostingDto.Id);

            var updateEmptransferPosting = _mapper.Map(request.UpdateEmpTransferPostingDto, EmpTransferPosting);

            if (updateEmptransferPosting.TransferApproveStatus == true && updateEmptransferPosting.DeptApproveStatus == true && updateEmptransferPosting.JoiningStatus == true)
            {
                updateEmptransferPosting.ApplicationStatus = true;
            }
            else if (updateEmptransferPosting.TransferApproveStatus == false || updateEmptransferPosting.DeptApproveStatus == false || updateEmptransferPosting.JoiningStatus == false)
            {
                updateEmptransferPosting.ApplicationStatus = false;
            }
            else
            {
                updateEmptransferPosting.ApplicationStatus = null;
            }

            await _unitOfWork.Repository<EmpTransferPosting>().Update(updateEmptransferPosting);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Application Update Successfull";
            response.Id = EmpTransferPosting.Id;



            return response;
        }

    }
}
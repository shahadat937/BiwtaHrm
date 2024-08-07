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
    public class UpdateEmpTransferPostingInfoCommandHandler : IRequestHandler<UpdateEmpTransferPostingInfoCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpTransferPosting> _EmpTransferPostingRepository;
        private readonly IHrmRepository<EmpJobDetail> _EmpEmpJobDetailsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpTransferPostingInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpTransferPosting> EmpTransferPostingRepository, IHrmRepository<EmpJobDetail> empEmpJobDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
            _EmpEmpJobDetailsRepository = empEmpJobDetailsRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmpTransferPostingInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpTransferPosting = await _unitOfWork.Repository<EmpTransferPosting>().Get(request.UpdateEmpTransferPostingDto.Id);

            var updateEmptransferPosting = _mapper.Map(request.UpdateEmpTransferPostingDto, EmpTransferPosting);


            var empJobDetailsInfo = await _EmpEmpJobDetailsRepository.FindOneAsync(x => x.EmpId == request.UpdateEmpTransferPostingDto.EmpId);
            var empJobDetails = await _unitOfWork.Repository<EmpJobDetail>().Get(empJobDetailsInfo.Id);

            if ((request.UpdateEmpTransferPostingDto.TransferApproveDate != null || request.UpdateEmpTransferPostingDto.IsTransferApprove == false) && (request.UpdateEmpTransferPostingDto.TransferApproveStatus == true || request.UpdateEmpTransferPostingDto.TransferApproveStatus == null))
            {
                updateEmptransferPosting.TransferApproveStatus = true;
            }
            if ((request.UpdateEmpTransferPostingDto.TransferApproveDate == null || request.UpdateEmpTransferPostingDto.IsTransferApprove == true) && (request.UpdateEmpTransferPostingDto.TransferApproveStatus == null))
            {
                updateEmptransferPosting.TransferApproveStatus = null;
            }
            if (request.UpdateEmpTransferPostingDto.TransferApproveStatus == false)
            {
                updateEmptransferPosting.TransferApproveStatus = false;
            }

            if ((request.UpdateEmpTransferPostingDto.DeptReleaseDate != null || request.UpdateEmpTransferPostingDto.IsDepartmentApprove == false) && (request.UpdateEmpTransferPostingDto.DeptClearance == true && (request.UpdateEmpTransferPostingDto.DeptApproveStatus == null || request.UpdateEmpTransferPostingDto.DeptApproveStatus == true)))
            {
                updateEmptransferPosting.DeptApproveStatus = true;
            }
            if ((request.UpdateEmpTransferPostingDto.DeptReleaseDate == null || request.UpdateEmpTransferPostingDto.IsDepartmentApprove == true) && (request.UpdateEmpTransferPostingDto.DeptApproveStatus == null))
            {
                updateEmptransferPosting.DeptApproveStatus = null;
            }
            if (request.UpdateEmpTransferPostingDto.DeptApproveStatus == false || request.UpdateEmpTransferPostingDto.DeptClearance == false)
            {
                updateEmptransferPosting.DeptApproveStatus = false;
            }

            if (request.UpdateEmpTransferPostingDto.JoiningDate != null || request.UpdateEmpTransferPostingDto.IsJoining == false)
            {
                updateEmptransferPosting.JoiningStatus = true;
            }
            if ((request.UpdateEmpTransferPostingDto.JoiningDate == null || request.UpdateEmpTransferPostingDto.IsJoining == true) && (request.UpdateEmpTransferPostingDto.JoiningStatus == null))
            {
                updateEmptransferPosting.JoiningStatus = null;
            }
            if (request.UpdateEmpTransferPostingDto.JoiningStatus == false)
            {
                updateEmptransferPosting.JoiningStatus = false;
            }

            if (updateEmptransferPosting.TransferApproveStatus == true && updateEmptransferPosting.DeptApproveStatus == true && updateEmptransferPosting.JoiningStatus == true)
            {
                updateEmptransferPosting.ApplicationStatus = true;

                //************ Update EmpJobDetails Info *************
                if (updateEmptransferPosting.TransferDepartmentId != null)
                {
                    empJobDetails.DepartmentId = updateEmptransferPosting.TransferDepartmentId;
                }
                if (updateEmptransferPosting.TransferDesignationId != null)
                {
                    empJobDetails.DesignationId = updateEmptransferPosting.TransferDesignationId;
                }
                if (updateEmptransferPosting.TransferSectionId != null)
                {
                    empJobDetails.SectionId = updateEmptransferPosting.TransferSectionId;
                }
                await _unitOfWork.Repository<EmpJobDetail>().Update(empJobDetails);
            }
            else if (updateEmptransferPosting.TransferApproveStatus == false || updateEmptransferPosting.DeptApproveStatus == false || updateEmptransferPosting.JoiningStatus == false)
            {
                updateEmptransferPosting.ApplicationStatus = false;

                //************ Revert EmpJobDetails Info *************
                if (empJobDetails.DepartmentId != updateEmptransferPosting.CurrentDepartmentId)
                {
                    empJobDetails.DepartmentId = updateEmptransferPosting.CurrentDepartmentId;
                }
                if (empJobDetails.DesignationId != updateEmptransferPosting.CurrentDesignationId)
                {
                    empJobDetails.DesignationId = updateEmptransferPosting.CurrentDesignationId;
                }
                if (empJobDetails.SectionId != updateEmptransferPosting.CurrentSectionId)
                {
                    empJobDetails.SectionId = updateEmptransferPosting.CurrentSectionId;
                }
                await _unitOfWork.Repository<EmpJobDetail>().Update(empJobDetails);
            }
            else
            {
                updateEmptransferPosting.ApplicationStatus = null;

                //************ Revert EmpJobDetails Info *************
                if (empJobDetails.DepartmentId != updateEmptransferPosting.CurrentDepartmentId)
                {
                    empJobDetails.DepartmentId = updateEmptransferPosting.CurrentDepartmentId;
                }
                if (empJobDetails.DesignationId != updateEmptransferPosting.CurrentDesignationId)
                {
                    empJobDetails.DesignationId = updateEmptransferPosting.CurrentDesignationId;
                }
                if (empJobDetails.SectionId != updateEmptransferPosting.CurrentSectionId)
                {
                    empJobDetails.SectionId = updateEmptransferPosting.CurrentSectionId;
                }
                await _unitOfWork.Repository<EmpJobDetail>().Update(empJobDetails);
            }


            await _unitOfWork.Repository<EmpTransferPosting>().Update(updateEmptransferPosting);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";
            response.Id = EmpTransferPosting.Id;



            return response;
        }

    }
}
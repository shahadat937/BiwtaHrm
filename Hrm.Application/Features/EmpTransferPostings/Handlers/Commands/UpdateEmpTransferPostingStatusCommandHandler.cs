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
        private readonly IHrmRepository<EmpJobDetail> _EmpEmpJobDetailsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpTransferPostingStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpTransferPosting> EmpTransferPostingRepository, IHrmRepository<EmpJobDetail> empEmpJobDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
            _EmpEmpJobDetailsRepository = empEmpJobDetailsRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmpTransferPostingStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpTransferPosting = await _unitOfWork.Repository<EmpTransferPosting>().Get(request.UpdateEmpTransferPostingDto.Id);

            var updateEmptransferPosting = _mapper.Map(request.UpdateEmpTransferPostingDto, EmpTransferPosting);


            var empJobDetailsInfo = await _EmpEmpJobDetailsRepository.FindOneAsync(x => x.EmpId == request.UpdateEmpTransferPostingDto.EmpId);
            var empJobDetails = await _unitOfWork.Repository<EmpJobDetail>().Get(empJobDetailsInfo.Id);

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

                if (updateEmptransferPosting.UpdateGradeId != null)
                {
                    empJobDetails.PresentGradeId = updateEmptransferPosting.UpdateGradeId;
                }
                if (updateEmptransferPosting.UpdateScaleId != null)
                {
                    empJobDetails.PresentScaleId = updateEmptransferPosting.UpdateScaleId;
                }
                if (updateEmptransferPosting.UpdateBasicPay != null)
                {
                    empJobDetails.BasicPay = updateEmptransferPosting.UpdateBasicPay;
                }
                empJobDetails.CurrentPositionJoinDate = updateEmptransferPosting.JoiningDate;
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

                if (empJobDetails.PresentGradeId != updateEmptransferPosting.CurrentGradeId)
                {
                    empJobDetails.PresentGradeId = updateEmptransferPosting.CurrentGradeId;
                }
                if (empJobDetails.PresentScaleId != updateEmptransferPosting.CurrentScaleId)
                {
                    empJobDetails.PresentScaleId = updateEmptransferPosting.CurrentScaleId;
                }
                if (empJobDetails.BasicPay != updateEmptransferPosting.CurrentBasicPay)
                {
                    empJobDetails.BasicPay = updateEmptransferPosting.CurrentBasicPay;
                }
                empJobDetails.CurrentPositionJoinDate = updateEmptransferPosting.CurrentDeptJoinDate;
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

                if (empJobDetails.PresentGradeId != updateEmptransferPosting.CurrentGradeId)
                {
                    empJobDetails.PresentGradeId = updateEmptransferPosting.CurrentGradeId;
                }
                if (empJobDetails.PresentScaleId != updateEmptransferPosting.CurrentScaleId)
                {
                    empJobDetails.PresentScaleId = updateEmptransferPosting.CurrentScaleId;
                }
                if (empJobDetails.BasicPay != updateEmptransferPosting.CurrentBasicPay)
                {
                    empJobDetails.BasicPay = updateEmptransferPosting.CurrentBasicPay;
                }
                empJobDetails.CurrentPositionJoinDate = updateEmptransferPosting.CurrentDeptJoinDate;
                await _unitOfWork.Repository<EmpJobDetail>().Update(empJobDetails);
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
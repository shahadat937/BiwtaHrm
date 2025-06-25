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
    public class CreateEmpTransferPostingCommandHandler : IRequestHandler<CreateEmpTransferPostingCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpJobDetail> _EmpEmpJobDetailsRepository;
        private readonly IHrmRepository<EmpOtherResponsibility> _EmpOtherResponsibilityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmpTransferPostingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpJobDetail> EmpEmpJobDetailsRepository, IHrmRepository<EmpOtherResponsibility> empOtherResponsibilityRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpEmpJobDetailsRepository = EmpEmpJobDetailsRepository;
            _EmpOtherResponsibilityRepository = empOtherResponsibilityRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpTransferPostingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var empTransferPostings = _mapper.Map<EmpTransferPosting>(request.EmpTransferPostingDto);


            var empJobDetailsInfo = await _EmpEmpJobDetailsRepository.FindOneAsync(x => x.EmpId == request.EmpTransferPostingDto.EmpId);
            var empJobDetails = await _unitOfWork.Repository<EmpJobDetail>().Get(empJobDetailsInfo.Id);

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
                empTransferPostings.JoiningStatus = true;
            }

            if (empTransferPostings.TransferApproveStatus == true && empTransferPostings.DeptApproveStatus == true && empTransferPostings.JoiningStatus == true)
            {
                empTransferPostings.ApplicationStatus = true;

                //************ Update EmpJobDetails Info *************

                if (request.EmpTransferPostingDto.IsAdditionalDesignation != true)
                {
                    
                   empJobDetails.DepartmentId = empTransferPostings.TransferDepartmentId;
                   empJobDetails.DesignationId = empTransferPostings.TransferDesignationId;
                   empJobDetails.SectionId = empTransferPostings.TransferSectionId;
                    empJobDetails.CurrentPositionJoinDate = empTransferPostings.JoiningDate;



                    if (empTransferPostings.UpdateGradeId != null)
                    {
                        empJobDetails.PresentGradeId = empTransferPostings.UpdateGradeId;
                    }
                    if (empTransferPostings.UpdateScaleId != null)
                    {
                        empJobDetails.PresentScaleId = empTransferPostings.UpdateScaleId;
                    }
                    if (empTransferPostings.UpdateBasicPay != null)
                    {
                        empJobDetails.BasicPay = empTransferPostings.UpdateBasicPay;
                    }
                    await _unitOfWork.Repository<EmpJobDetail>().Update(empJobDetails);
                }
            

            }
            else if (request.EmpTransferPostingDto.TransferApproveStatus == false || request.EmpTransferPostingDto.DeptApproveStatus == false || (request.EmpTransferPostingDto.JoiningStatus == false && request.EmpTransferPostingDto.JoiningDate == null))
            {
                empTransferPostings.ApplicationStatus = false;

                //************ Revert EmpJobDetails Info *************
                if(request.EmpTransferPostingDto.IsAdditionalDesignation != true) {
                    
                        empJobDetails.DepartmentId = empTransferPostings.CurrentDepartmentId;                   
                        empJobDetails.DesignationId = empTransferPostings.CurrentDesignationId;                    
                        empJobDetails.SectionId = empTransferPostings.CurrentSectionId;
                    empJobDetails.CurrentPositionJoinDate = empTransferPostings.CurrentDeptJoinDate;


                    if (empJobDetails.PresentGradeId != empTransferPostings.CurrentGradeId)
                    {
                        empJobDetails.PresentGradeId = empTransferPostings.CurrentGradeId;
                    }
                    if (empJobDetails.PresentScaleId != empTransferPostings.CurrentScaleId)
                    {
                        empJobDetails.PresentScaleId = empTransferPostings.CurrentScaleId;
                    }
                    if (empJobDetails.BasicPay != empTransferPostings.CurrentBasicPay)
                    {
                        empJobDetails.BasicPay = empTransferPostings.CurrentBasicPay;
                    }
                    await _unitOfWork.Repository<EmpJobDetail>().Update(empJobDetails);
                }

            }
            else
            {
                empTransferPostings.ApplicationStatus = null;
            }


            //-------------------- If Designation is Additional --------------------------//

            if (request.EmpTransferPostingDto.IsAdditionalDesignation == true)
            {
                var otherDesignation = _EmpOtherResponsibilityRepository.FindOne(x => x.EmpId == request.EmpTransferPostingDto.EmpId && x.SectionId == request.EmpTransferPostingDto.CurrentSectionId && x.DesignationId == request.EmpTransferPostingDto.CurrentDesignationId);

                if (otherDesignation != null && empTransferPostings.TransferApproveStatus == true && empTransferPostings.DeptApproveStatus == true && empTransferPostings.JoiningStatus == true)
                {
                    
                        otherDesignation.DepartmentId = empTransferPostings.TransferDepartmentId;
                        otherDesignation.DesignationId = empTransferPostings.TransferDesignationId;
                        otherDesignation.SectionId = empTransferPostings.TransferSectionId;
                        otherDesignation.ResponsibilityTypeId = request.EmpTransferPostingDto.TransferResponsibilityTypeId;

                }
                else if (otherDesignation != null && request.EmpTransferPostingDto.TransferApproveStatus == false || request.EmpTransferPostingDto.DeptApproveStatus == false || (request.EmpTransferPostingDto.JoiningStatus == false && request.EmpTransferPostingDto.JoiningDate == null))
                {
                    
                    otherDesignation.DepartmentId = empTransferPostings.TransferDepartmentId;
                    otherDesignation.DesignationId = empTransferPostings.TransferDesignationId;
                    otherDesignation.SectionId = empTransferPostings.TransferSectionId;
                    otherDesignation.ResponsibilityTypeId = request.EmpTransferPostingDto.TransferResponsibilityTypeId;
                    
                }
                empTransferPostings.IsAdditionalDesignation = true;
                await _unitOfWork.Repository<EmpOtherResponsibility>().Update(otherDesignation);
            }


            await _unitOfWork.Repository<EmpTransferPosting>().Add(empTransferPostings);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = empTransferPostings.Id;

            return response;
        }

    }
}

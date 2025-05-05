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
        private readonly IHrmRepository<EmpOtherResponsibility> _EmpOtherResponsibilityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpTransferPostingInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpTransferPosting> EmpTransferPostingRepository, IHrmRepository<EmpJobDetail> empEmpJobDetailsRepository, IHrmRepository<EmpOtherResponsibility> empOtherResponsibilityRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpTransferPostingRepository = EmpTransferPostingRepository;
            _EmpEmpJobDetailsRepository = empEmpJobDetailsRepository;
            _EmpOtherResponsibilityRepository = empOtherResponsibilityRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmpTransferPostingInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpTransferPosting = await _unitOfWork.Repository<EmpTransferPosting>().Get(request.UpdateEmpTransferPostingDto.Id);

            var updateEmptransferPosting = _mapper.Map(request.UpdateEmpTransferPostingDto, EmpTransferPosting);


       

            var empJobDetailsInfo = await _EmpEmpJobDetailsRepository.FindOneAsync(x => x.EmpId == request.UpdateEmpTransferPostingDto.EmpId);
            var empJobDetails = await _unitOfWork.Repository<EmpJobDetail>().Get(empJobDetailsInfo.Id);


            var empId = EmpTransferPosting.EmpId;

            var otherDesignation = _EmpOtherResponsibilityRepository.FindOne(x =>
                x.EmpId == empId &&
                x.SectionId == EmpTransferPosting.TransferSectionId &&
                x.DesignationId == EmpTransferPosting.TransferDesignationId &&
                x.DepartmentId == EmpTransferPosting.TransferDepartmentId
            );

            if (otherDesignation == null)
            {
                otherDesignation = _EmpOtherResponsibilityRepository.FindOne(x =>
                    x.EmpId == empId &&
                    x.SectionId == EmpTransferPosting.CurrentSectionId &&
                    x.DesignationId == EmpTransferPosting.CurrentDesignationId &&
                    x.DepartmentId == EmpTransferPosting.CurrentDepartmentId
                );
            }

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

                if(EmpTransferPosting.IsAdditionalDesignation != true)
                {
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
                    await _unitOfWork.Repository<EmpJobDetail>().Update(empJobDetails);

                }
                else
                {

                    if (otherDesignation != null)
                    {
                        otherDesignation.DepartmentId = updateEmptransferPosting.TransferDepartmentId;
                        otherDesignation.SectionId = updateEmptransferPosting.TransferSectionId;
                        otherDesignation.DesignationId = updateEmptransferPosting.TransferDesignationId;
                        otherDesignation.ResponsibilityTypeId = updateEmptransferPosting.CurrentResponsibiltyTypeId;
                        await _unitOfWork.Repository<EmpOtherResponsibility>().Update(otherDesignation);
                    }


                }


            }
            else if (updateEmptransferPosting.TransferApproveStatus == false || updateEmptransferPosting.DeptApproveStatus == false || updateEmptransferPosting.JoiningStatus == false)
            {
                updateEmptransferPosting.ApplicationStatus = false;

                if (EmpTransferPosting.IsAdditionalDesignation != true)
                {

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
                    await _unitOfWork.Repository<EmpJobDetail>().Update(empJobDetails);
                }
                else
                {
                    if(EmpTransferPosting.TransferApproveStatus == true && EmpTransferPosting.DeptApproveStatus == true && EmpTransferPosting.JoiningStatus == true)
                    {

                        if (otherDesignation != null)
                        {
                            otherDesignation.DepartmentId = updateEmptransferPosting.TransferDepartmentId;
                            otherDesignation.SectionId = updateEmptransferPosting.TransferSectionId;
                            otherDesignation.DesignationId = updateEmptransferPosting.TransferDesignationId;
                            otherDesignation.ResponsibilityTypeId = updateEmptransferPosting.CurrentResponsibiltyTypeId;
                            await _unitOfWork.Repository<EmpOtherResponsibility>().Update(otherDesignation);
                        }
                    }
                }
            }
            else
            {
                updateEmptransferPosting.ApplicationStatus = null;

                if (EmpTransferPosting.IsAdditionalDesignation != true)
                {
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
                    await _unitOfWork.Repository<EmpJobDetail>().Update(empJobDetails);
                }
                else
                {
                    if (otherDesignation != null)
                    {
                        otherDesignation.DepartmentId = updateEmptransferPosting.TransferDepartmentId;
                        otherDesignation.SectionId = updateEmptransferPosting.TransferSectionId;
                        otherDesignation.DesignationId = updateEmptransferPosting.TransferDesignationId;
                        otherDesignation.ResponsibilityTypeId = updateEmptransferPosting.CurrentResponsibiltyTypeId;
                        await _unitOfWork.Repository<EmpOtherResponsibility>().Update(otherDesignation);
                    }
                }
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
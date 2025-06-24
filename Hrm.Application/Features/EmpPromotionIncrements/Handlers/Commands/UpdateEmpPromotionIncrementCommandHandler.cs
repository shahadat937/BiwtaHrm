using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpPromotionIncrements.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPromotionIncrements.Handlers.Commands
{
    public class UpdateEmpPromotionIncrementCommandHandler : IRequestHandler<UpdateEmpPromotionIncrementCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpJobDetail> _EmpEmpJobDetailsRepository;
        private readonly IHrmRepository<EmpPromotionIncrement> _EmpEmpPromotionIncrementRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpPromotionIncrementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpJobDetail> EmpEmpJobDetailsRepository, IHrmRepository<EmpPromotionIncrement> empEmpPromotionIncrementRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpEmpJobDetailsRepository = EmpEmpJobDetailsRepository;
            _EmpEmpPromotionIncrementRepository = empEmpPromotionIncrementRepository;
        }
        public async Task<BaseCommandResponse> Handle(UpdateEmpPromotionIncrementCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpPromotionIncrements = await _unitOfWork.Repository<EmpPromotionIncrement>().Get(request.EmpPromotionIncrementDto.Id);

            var updateEmpPromotionIncrement = _mapper.Map(request.EmpPromotionIncrementDto, EmpPromotionIncrements);


            var empJobDetailsInfo = await _EmpEmpJobDetailsRepository.FindOneAsync(x => x.EmpId == request.EmpPromotionIncrementDto.EmpId);
            var empJobDetails = await _unitOfWork.Repository<EmpJobDetail>().Get(empJobDetailsInfo.Id);

            if (updateEmpPromotionIncrement.IsApproval != true || updateEmpPromotionIncrement.ApproveStatus == true)
            {
                updateEmpPromotionIncrement.ApplicationStatus = true;

                //************ Update EmpJobDetails Info *************
                if (updateEmpPromotionIncrement.UpdateDesignationId != null)
                {
                    empJobDetails.DesignationId = updateEmpPromotionIncrement.UpdateDesignationId;
                    empJobDetails.CurrentPositionJoinDate = updateEmpPromotionIncrement.EffectiveDate;
                }
                if (updateEmpPromotionIncrement.UpdateGradeId != null)
                {
                    empJobDetails.PresentGradeId = updateEmpPromotionIncrement.UpdateGradeId;
                }
                if (updateEmpPromotionIncrement.UpdateScaleId != null)
                {
                    empJobDetails.PresentScaleId = updateEmpPromotionIncrement.UpdateScaleId;
                }
                if (updateEmpPromotionIncrement.UpdateBasicPay != null)
                {
                    empJobDetails.BasicPay = updateEmpPromotionIncrement.UpdateBasicPay;
                }
                await _unitOfWork.Repository<EmpJobDetail>().Update(empJobDetails);
            }
            else if (updateEmpPromotionIncrement.IsApproval == true && updateEmpPromotionIncrement.ApproveStatus == false)
            {
                updateEmpPromotionIncrement.ApplicationStatus = false;

                //************ Update EmpJobDetails Info *************
                if (empJobDetails.DesignationId != updateEmpPromotionIncrement.CurrentDesignationId)
                {
                    empJobDetails.DesignationId = updateEmpPromotionIncrement.CurrentDesignationId;
                    empJobDetails.CurrentPositionJoinDate = updateEmpPromotionIncrement.CurrentDeptJoinDate;
                }
                if (empJobDetails.PresentGradeId != updateEmpPromotionIncrement.CurrentGradeId)
                {
                    empJobDetails.PresentGradeId = updateEmpPromotionIncrement.CurrentGradeId;
                }
                if (empJobDetails.PresentScaleId != updateEmpPromotionIncrement.CurrentScaleId)
                {
                    empJobDetails.PresentScaleId = updateEmpPromotionIncrement.CurrentScaleId;
                }
                if (empJobDetails.BasicPay != updateEmpPromotionIncrement.CurrentBasicPay)
                {
                    empJobDetails.BasicPay = updateEmpPromotionIncrement.CurrentBasicPay;
                }
                await _unitOfWork.Repository<EmpJobDetail>().Update(empJobDetails);
            }
            else if (updateEmpPromotionIncrement.ApproveStatus == null)
            {
                updateEmpPromotionIncrement.ApplicationStatus = null;

                //************ Update EmpJobDetails Info *************
                if (empJobDetails.DesignationId != updateEmpPromotionIncrement.CurrentDesignationId)
                {
                    empJobDetails.DesignationId = updateEmpPromotionIncrement.CurrentDesignationId;
                    empJobDetails.CurrentPositionJoinDate = updateEmpPromotionIncrement.CurrentDeptJoinDate;
                }
                if (empJobDetails.PresentGradeId != updateEmpPromotionIncrement.CurrentGradeId)
                {
                    empJobDetails.PresentGradeId = updateEmpPromotionIncrement.CurrentGradeId;
                }
                if (empJobDetails.PresentScaleId != updateEmpPromotionIncrement.CurrentScaleId)
                {
                    empJobDetails.PresentScaleId = updateEmpPromotionIncrement.CurrentScaleId;
                }
                if (empJobDetails.BasicPay != updateEmpPromotionIncrement.CurrentBasicPay)
                {
                    empJobDetails.BasicPay = updateEmpPromotionIncrement.CurrentBasicPay;
                }
                await _unitOfWork.Repository<EmpJobDetail>().Update(empJobDetails);
            }

            await _unitOfWork.Repository<EmpPromotionIncrement>().Update(updateEmpPromotionIncrement);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Update Successful";

            return response;
        }

    }
}

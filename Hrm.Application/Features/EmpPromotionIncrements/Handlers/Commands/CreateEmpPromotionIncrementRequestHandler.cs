using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Shift.Validators;
using Hrm.Application.Features.EmpPromotionIncrements.Requests.Commands;
using Hrm.Application.Features.Shift.Requests.Commands;
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
    public class CreateEmpPromotionIncrementRequestHandler : IRequestHandler<CreateEmpPromotionIncrementRequest, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpJobDetail> _EmpEmpJobDetailsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmpPromotionIncrementRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpJobDetail> EmpEmpJobDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpEmpJobDetailsRepository = EmpEmpJobDetailsRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpPromotionIncrementRequest request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpPromotionIncrements = _mapper.Map<EmpPromotionIncrement>(request.EmpPromotionIncrementDto);


            var empJobDetailsInfo = await _EmpEmpJobDetailsRepository.FindOneAsync(x => x.EmpId == request.EmpPromotionIncrementDto.EmpId);
            var empJobDetails = await _unitOfWork.Repository<EmpJobDetail>().Get(empJobDetailsInfo.Id);

            if (EmpPromotionIncrements.IsApproval != true || EmpPromotionIncrements.ApproveStatus == true)
            {
                EmpPromotionIncrements.ApplicationStatus = true;

                //************ Update EmpJobDetails Info *************
                if (EmpPromotionIncrements.UpdateDesignationId != null)
                {
                    empJobDetails.DesignationId = EmpPromotionIncrements.UpdateDesignationId;
                }
                if (EmpPromotionIncrements.CurrentGradeId != null)
                {
                    empJobDetails.PresentGradeId = EmpPromotionIncrements.CurrentGradeId;
                }
                if (EmpPromotionIncrements.CurrentScaleId != null)
                {
                    empJobDetails.PresentScaleId = EmpPromotionIncrements.CurrentScaleId;
                }
                if (EmpPromotionIncrements.CurrentBasicPay != null)
                {
                    empJobDetails.BasicPay = EmpPromotionIncrements.CurrentBasicPay;
                }
                await _unitOfWork.Repository<EmpJobDetail>().Update(empJobDetails);
            }
            else if (EmpPromotionIncrements.IsApproval == true && EmpPromotionIncrements.ApproveStatus == false)
            {
                EmpPromotionIncrements.ApplicationStatus = false;
            }
            else if (EmpPromotionIncrements.ApproveStatus == null)
            {
                EmpPromotionIncrements.ApplicationStatus = null;
            }

            await _unitOfWork.Repository<EmpPromotionIncrement>().Add(EmpPromotionIncrements);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";

            return response;
        }

    }
}

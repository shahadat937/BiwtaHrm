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
    public class CreateEmpPromotionIncrementCommandHandler : IRequestHandler<CreateEmpPromotionIncrementCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<EmpJobDetail> _EmpEmpJobDetailsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEmpPromotionIncrementCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpJobDetail> EmpEmpJobDetailsRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpEmpJobDetailsRepository = EmpEmpJobDetailsRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpPromotionIncrementCommand request, CancellationToken cancellationToken)
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
                if (EmpPromotionIncrements.UpdateGradeId != null)
                {
                    empJobDetails.PresentGradeId = EmpPromotionIncrements.UpdateGradeId;
                }
                if (EmpPromotionIncrements.UpdateScaleId != null)
                {
                    empJobDetails.PresentScaleId = EmpPromotionIncrements.UpdateScaleId;
                }
                if (EmpPromotionIncrements.UpdateBasicPay != null)
                {
                    empJobDetails.BasicPay = EmpPromotionIncrements.UpdateBasicPay;
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

            response.Id = EmpPromotionIncrements.Id;
            response.Success = true;
            response.Message = "Creation Successful";

            return response;
        }

    }
}

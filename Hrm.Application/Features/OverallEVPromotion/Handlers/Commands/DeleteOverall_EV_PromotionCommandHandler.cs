using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Overall_EV_Promotion.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Overall_EV_Promotion.Handlers.Commands
{
    public class DeleteOverall_EV_PromotionCommandHandler : IRequestHandler<DeleteOverall_EV_PromotionCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteOverall_EV_PromotionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteOverall_EV_PromotionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Overall_EV_Promotion = await _unitOfWork.Repository<Hrm.Domain.OverallEVPromotion>().Get(request.Overall_EV_PromotionId);

            if (Overall_EV_Promotion == null)
            {
                throw new NotFoundException(nameof(Overall_EV_Promotion), request.Overall_EV_PromotionId);
            }

            await _unitOfWork.Repository<Hrm.Domain.OverallEVPromotion>().Delete(Overall_EV_Promotion);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Overall_EV_Promotion.OverallEVPromotionId;

            return response;
        }
    }
}

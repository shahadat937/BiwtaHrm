using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.PromotionType.Request.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PromotionType.Handler.Commands
{
    public class DeletePromotionTypeCommandHandler : IRequestHandler<DeletePromotionTypeCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeletePromotionTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeletePromotionTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var PromotionType = await _unitOfWork.Repository<Hrm.Domain.PromotionType>().Get(request.PromotionTypeId);

            if (PromotionType == null)
            {
                throw new NotFoundException(nameof(PromotionType), request.PromotionTypeId);
            }

            await _unitOfWork.Repository<Hrm.Domain.PromotionType>().Delete(PromotionType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = PromotionType.PromotionTypeId;

            return response;
        }
    }
}

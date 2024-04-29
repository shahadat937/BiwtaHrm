using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.PostingOrderInfo.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PostingOrderInfo.Handlers.Commands
{
    public class DeletePostingOrderInfoCommandHandler : IRequestHandler<DeletePostingOrderInfoCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeletePostingOrderInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeletePostingOrderInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var PostingOrderInfo = await _unitOfWork.Repository<Hrm.Domain.PostingOrderInfo>().Get(request.PostingOrderInfoId);

            if (PostingOrderInfo == null)
            {
                throw new NotFoundException(nameof(PostingOrderInfo), request.PostingOrderInfoId);
            }

            await _unitOfWork.Repository<Hrm.Domain.PostingOrderInfo>().Delete(PostingOrderInfo);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = PostingOrderInfo.PostingOrderInfoId;

            return response;
        }
    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.PostingTypes.Request.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PostingTypes.Handler.Commands
{
    public class DeletePostingTypeCommandHandler : IRequestHandler<DeletePostingTypeCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeletePostingTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeletePostingTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var PostingType = await _unitOfWork.Repository<Hrm.Domain.PostingType>().Get(request.PostingTypeId);

            if (PostingType == null)
            {
                throw new NotFoundException(nameof(PostingType), request.PostingTypeId);
            }

            await _unitOfWork.Repository<Hrm.Domain.PostingType>().Delete(PostingType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = PostingType.Id;

            return response;
        }
    }
}

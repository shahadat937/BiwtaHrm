using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.TrainingType.Requests.Commands;
using Hrm.Application.Features.Ward.Request.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Ward.Handler.Commands
{
    public class DeleteWardCommandHandler : IRequestHandler<DeleteWardCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteWardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteWardCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Ward = await _unitOfWork.Repository<Hrm.Domain.Ward>().Get(request.WardId);

            if (Ward == null)
            {
                throw new NotFoundException(nameof(Ward), request.WardId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Ward>().Delete(Ward);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Ward.WardId;

            return response;
        }
    }
}
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Result.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Result.Handlers.Commands
{
    public class DeleteResultCommandHandler : IRequestHandler<DeleteResultCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteResultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteResultCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Result = await _unitOfWork.Repository<Hrm.Domain.Result>().Get(request.ResultId);

            if (Result == null)
            {
                throw new NotFoundException(nameof(Result), request.ResultId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Result>().Delete(Result);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Result.ResultId;

            return response;
        }
    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.MaritalStatus.Handlers.Commands
{
    public class DeleteMaritalStatusCommandHandler : IRequestHandler<DeleteMaritalStatusCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteMaritalStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteMaritalStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var MaritalStatus = await _unitOfWork.Repository<Hrm.Domain.MaritalStatus>().Get(request.MaritalStatusId);

            if (MaritalStatus == null)
            {
                throw new NotFoundException(nameof(MaritalStatus), request.MaritalStatusId);
            }

            await _unitOfWork.Repository<Hrm.Domain.MaritalStatus>().Delete(MaritalStatus);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = MaritalStatus.MaritalStatusId;

            return response;
        }
    }
}

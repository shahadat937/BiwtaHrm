using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.ResponsibilityType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ResponsibilityType.Handlers.Commands
{
    public class DeleteResponsibilityTypeCommandHandler : IRequestHandler<DeleteResponsibilityTypeCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteResponsibilityTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteResponsibilityTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var ResponsibilityType = await _unitOfWork.Repository<Hrm.Domain.ResponsibilityType>().Get(request.ResponsibilityTypeId);

            if (ResponsibilityType == null)
            {
                throw new NotFoundException(nameof(ResponsibilityType), request.ResponsibilityTypeId);
            }

            await _unitOfWork.Repository<Hrm.Domain.ResponsibilityType>().Delete(ResponsibilityType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = ResponsibilityType.Id;

            return response;
        }
    }
}

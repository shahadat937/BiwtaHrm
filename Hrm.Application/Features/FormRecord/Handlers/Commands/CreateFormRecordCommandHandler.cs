using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormRecord.Validators;
using Hrm.Application.Features.FormRecord.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormRecord.Handlers.Commands
{
    public class CreateFormRecordCommandHandler: IRequestHandler<CreateFormRecordCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public CreateFormRecordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFormRecordCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFormRecordDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FormRecordDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var formRecord = _mapper.Map<Hrm.Domain.FormRecord>(request.FormRecordDto);

            await _unitOfWork.Repository<Hrm.Domain.FormRecord>().Add(formRecord);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = formRecord.RecordId;

            return response;
        }
    }
}

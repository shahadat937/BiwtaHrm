using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormRecord;
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
    public class UpdateFormRecordCommandHandler: IRequestHandler<UpdateFormRecordCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFormRecordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateFormRecordCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateFormRecordDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FormRecordDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var formRecord = await _unitOfWork.Repository<Hrm.Domain.FormRecord>().Get(request.FormRecordDto.RecordId);

            if(formRecord == null)
            {
                throw new NotFoundException(nameof(formRecord),request.FormRecordDto.RecordId);
            }

            _mapper.Map(request.FormRecordDto, formRecord);

            await _unitOfWork.Repository<Hrm.Domain.FormRecord>().Update(formRecord);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = formRecord.RecordId;

            return response;
        }
    }
}

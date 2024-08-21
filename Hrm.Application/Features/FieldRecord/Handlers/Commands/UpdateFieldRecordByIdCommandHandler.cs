using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FieldRecord.Validators;
using Hrm.Application.Features.FieldRecord.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Hrm.Application.Features.FieldRecord.Handlers.Commands
{
    public class UpdateFieldRecordByIdCommandHandler: IRequestHandler<UpdateFieldRecordCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFieldRecordByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateFieldRecordCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateFieldRecordDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FieldRecordDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            if (await CheckDataType(request.FieldRecordDto.FieldValue, request.FieldRecordDto.FieldId) == false)
            {
                throw new BadRequestException("Data type is invalid");
            }

            var fieldRecord = await _unitOfWork.Repository<Hrm.Domain.FieldRecord>().Get(request.FieldRecordDto.FieldRecordId);

            if(fieldRecord == null)
            {
                throw new NotFoundException(nameof(fieldRecord),request.FieldRecordDto.FieldRecordId);
            }

            _mapper.Map(request.FieldRecordDto, fieldRecord);

            var formField = await _unitOfWork.Repository<Hrm.Domain.FormField>().Get(request.FieldRecordDto.FieldId);

            if (formField!=null&&(formField.HasMultipleValue == null||formField.HasMultipleValue==false))
            {
                var check = await _unitOfWork.Repository<Hrm.Domain.FieldRecord>().Where(x => x.FormRecordId == request.FieldRecordDto.FormRecordId && x.FieldId == request.FieldRecordDto.FieldId).AnyAsync();

                if (check)
                {
                    throw new BadRequestException("Multiple value for this field is not allowed");
                }
            }

            await _unitOfWork.Repository<Hrm.Domain.FieldRecord>().Update(fieldRecord);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = fieldRecord.FieldRecordId;

            return response;
        }

        public async Task<bool> CheckDataType(string value, int FieldId)
        {
            var field = await _unitOfWork.Repository<Hrm.Domain.FormField>().Where(x => x.FieldId == FieldId)
               .Include(x => x.FieldType)
               .FirstOrDefaultAsync();

            if (field == null)
            {
                return true;
            }

            var ValidationRegex = field.FieldType.ValidationRegex;

            if (ValidationRegex == null)
            {
                return true;
            }

            string pattern = $@"{ValidationRegex}";
            Match match = Regex.Match(value, pattern);
            if (match == Match.Empty)
            {
                return false;
            }

            return true;
        }
    }
}

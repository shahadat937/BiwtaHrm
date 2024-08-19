using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormSchema.Validators;
using Hrm.Application.Features.FormSchema.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormSchema.Handlers.Commands
{
    public class UpdateFormSchemaCommandHandler: IRequestHandler<UpdateFormSchemaCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFormSchemaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateFormSchemaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateFormSchemaDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FormSchemaDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var formSchema = await _unitOfWork.Repository<Hrm.Domain.FormSchema>().Get(request.FormSchemaDto.SchemaId);

            if(formSchema == null)
            {
                throw new NotFoundException(nameof(formSchema), request.FormSchemaDto.SchemaId);
            }

            _mapper.Map(request.FormSchemaDto, formSchema);

            await _unitOfWork.Repository<Hrm.Domain.FormSchema>().Update(formSchema);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = formSchema.SchemaId;

            return response;
        }
    }
}

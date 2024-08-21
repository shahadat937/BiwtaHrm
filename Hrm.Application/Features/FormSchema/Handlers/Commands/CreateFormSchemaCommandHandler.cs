using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormSchema.Validators;
using Hrm.Application.Features.FormSchema.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using MediatR;

namespace Hrm.Application.Features.FormSchema.Handlers.Commands
{
    public class CreateFormSchemaCommandHandler: IRequestHandler<CreateFormSchemaCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFormSchemaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFormSchemaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFormSchemaDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FormSchemaDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var formSchema = _mapper.Map<Hrm.Domain.FormSchema>(request.FormSchemaDto);
            await _unitOfWork.Repository<Hrm.Domain.FormSchema>().Add(formSchema);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = formSchema.SchemaId;

            return response;
        }
    }
}

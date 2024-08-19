using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Form.Validators;
using Hrm.Application.Features.Form.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Form.Handlers.Commands
{
    public class CreateFormCommandHandler: IRequestHandler<CreateFormCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateFormCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateFormCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateFormDtoValidator();
            var validationResult = await validator.ValidateAsync(request.formDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var formData = _mapper.Map<Hrm.Domain.Form>(request.formDto);

            await _unitOfWork.Repository<Hrm.Domain.Form>().Add(formData);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = formData.FormId;

            return response;
        }
    }
}

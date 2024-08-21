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
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Form.Handlers.Commands
{
    public class UpdateFormCommandHandler: IRequestHandler<UpdateFormCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFormCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateFormCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateFormDtoValidator();
            var validationResult = await validator.ValidateAsync(request.formDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var formData = await _unitOfWork.Repository<Hrm.Domain.Form>().Get(request.formDto.FormId);

            if(formData == null)
            {
                throw new NotFoundException(nameof(formData), request.formDto.FormId);
            }

            _mapper.Map(request.formDto, formData);

            await _unitOfWork.Repository<Hrm.Domain.Form>().Update(formData);

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = formData.FormId;

            return response;
        }
    }
}

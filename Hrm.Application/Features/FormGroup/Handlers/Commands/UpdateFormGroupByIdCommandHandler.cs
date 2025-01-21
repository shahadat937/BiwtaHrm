using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.FormGroup.Validators;
using Hrm.Application.Features.FormGroup.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;

namespace Hrm.Application.Features.FormGroup.Handlers.Commands
{
    public class UpdateFormGroupByIdCommandHandler : IRequestHandler<UpdateFormGroupByIdCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateFormGroupByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateFormGroupByIdCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new FormGroupDtoValidator();
            var validationResult = await validator.ValidateAsync(request.FormGroup);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var FormGroup = await _unitOfWork.Repository<Hrm.Domain.FormGroup>().Get(request.FormGroup.FormGroupId);

            if(FormGroup == null)
            {
                throw new NotFoundException(nameof(FormGroup), request.FormGroup.FormGroupId);
            }

            _mapper.Map(request.FormGroup, FormGroup);

            await _unitOfWork.Repository<Hrm.Domain.FormGroup>().Update(FormGroup);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = request.FormGroup.FormGroupId;

            return response;
        }
    }
}

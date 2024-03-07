using AutoMapper;
using FluentValidation;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup.Validators;
using Hrm.Application.DTOs.MaritalStatus.Validators;
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
    public class CreateMaritalStatusCommandHandler : IRequestHandler<CreateMaritalStatusCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMaritalStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<BaseCommandResponse> Handle(CreateMaritalStatusCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new CreateMaritalStatusDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.MaritalStatusDto);

            if (validatorResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validatorResult.Errors.Select(x=> x.ErrorMessage).ToList();
            }
            else
            {
                var MaritalStatus = _mapper.Map<Hrm.Domain.MaritalStatus>(request.MaritalStatusDto);

                MaritalStatus = await _unitOfWork.Repository<Hrm.Domain.MaritalStatus>().Add(MaritalStatus);
                await _unitOfWork.Save();

                respose.Success = true;
                respose.Message = "Creation Successfull";
                respose.Id = MaritalStatus.MaritalStatusId;
            }

            return respose;
        }
    }
}

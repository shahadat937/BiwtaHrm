using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Gender.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Gender.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Gender.Handlers.Commands
{
    public class UpdateGenderCommandHandler : IRequestHandler<UpdateGenderCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateGenderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateGenderCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateGenderDtoValidator();
            var validationResult = await validator.ValidateAsync(request.GenderDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Gender = await _unitOfWork.Repository<Hrm.Domain.Gender>().Get(request.GenderDto.GenderId);

            if (Gender is null)
            {
                throw new NotFoundException(nameof(Gender), request.GenderDto.GenderId);
            }

            _mapper.Map(request.GenderDto, Gender);

            await _unitOfWork.Repository<Hrm.Domain.Gender>().Update(Gender);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

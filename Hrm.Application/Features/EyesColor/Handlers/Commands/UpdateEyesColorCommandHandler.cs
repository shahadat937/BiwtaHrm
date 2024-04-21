using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EyesColor.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.EyesColor.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EyesColor.Handlers.Commands
{
    public class UpdateEyesColorCommandHandler : IRequestHandler<UpdateEyesColorCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEyesColorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEyesColorCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateEyesColorDtoValidator();
            var validationResult = await validator.ValidateAsync(request.EyesColorDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var EyesColor = await _unitOfWork.Repository<Hrm.Domain.EyesColor>().Get(request.EyesColorDto.EyesColorId);

            if (EyesColor is null)
            {
                throw new NotFoundException(nameof(EyesColor), request.EyesColorDto.EyesColorId);
            }

            _mapper.Map(request.EyesColorDto, EyesColor);

            await _unitOfWork.Repository<Hrm.Domain.EyesColor>().Update(EyesColor);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

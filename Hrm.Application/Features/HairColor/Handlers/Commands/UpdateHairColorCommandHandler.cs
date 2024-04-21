using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.HairColor.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.HairColor.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.HairColor.Handlers.Commands
{
    public class UpdateHairColorCommandHandler : IRequestHandler<UpdateHairColorCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateHairColorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateHairColorCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateHairColorDtoValidator();
            var validationResult = await validator.ValidateAsync(request.HairColorDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var HairColor = await _unitOfWork.Repository<Hrm.Domain.HairColor>().Get(request.HairColorDto.HairColorId);

            if (HairColor is null)
            {
                throw new NotFoundException(nameof(HairColor), request.HairColorDto.HairColorId);
            }

            _mapper.Map(request.HairColorDto, HairColor);

            await _unitOfWork.Repository<Hrm.Domain.HairColor>().Update(HairColor);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

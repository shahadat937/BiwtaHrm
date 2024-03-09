using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BloodGroup.Handlers.Commands
{
    public class UpdateBloodGroupCommandHandler : IRequestHandler<UpdateBloodGroupCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBloodGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBloodGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateBloodGroupDtoValidators();
            var validationResult = await validator.ValidateAsync(request.BloodGroupDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var BloodGroup = await _unitOfWork.Repository<Hrm.Domain.BloodGroup>().Get(request.BloodGroupDto.BloodGroupId);

            if (BloodGroup is null)
            {
                throw new NotFoundException(nameof(BloodGroup), request.BloodGroupDto.BloodGroupId);
            }

            _mapper.Map(request.BloodGroupDto, BloodGroup);

            await _unitOfWork.Repository<Hrm.Domain.BloodGroup>().Update(BloodGroup);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

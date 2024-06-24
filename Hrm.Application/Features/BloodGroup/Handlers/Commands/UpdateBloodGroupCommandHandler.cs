using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup.Validators;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BloodGroup.Handlers.Commands
{
    public class UpdateBloodGroupCommandHandler : IRequestHandler<UpdateBloodGroupCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.BloodGroup> _BloodGroupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBloodGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.BloodGroup> BloodGroupRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _BloodGroupRepository = BloodGroupRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateBloodGroupCommand request, CancellationToken cancellationToken)
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

            //var BloodGroupName = request.BloodGroupDto.BloodGroupName.ToLower();
            var BloodGroupName = request.BloodGroupDto.BloodGroupName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.BloodGroup> BloodGroups = _BloodGroupRepository.Where(x => x.BloodGroupName.ToLower() == BloodGroupName);



            if (BloodGroups.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.BloodGroupDto.BloodGroupName}' already exists.";

                //response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var BloodGroup = await _unitOfWork.Repository<Hrm.Domain.BloodGroup>().Get(request.BloodGroupDto.BloodGroupId);

                if (BloodGroup is null)
                {
                    throw new NotFoundException(nameof(BloodGroup), request.BloodGroupDto.BloodGroupId);
                }

                _mapper.Map(request.BloodGroupDto, BloodGroup);

                await _unitOfWork.Repository<Hrm.Domain.BloodGroup>().Update(BloodGroup);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = BloodGroup.BloodGroupId;

            }

            return response;
        }
    }
}

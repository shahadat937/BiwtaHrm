using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Group.Validators;
using Hrm.Application.DTOs.Group.Validators;
using Hrm.Application.DTOs.Group.ValidatorsGroup;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Group.Requests.Commands;
using Hrm.Application.Features.Group.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Group.Handlers.Commands
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.SubGroup> _GroupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.SubGroup> GroupRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _GroupRepository = GroupRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateGroupDtoValidator();
            var validationResult = await validator.ValidateAsync(request.GroupDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            //var GroupName = request.GroupDto.GroupName.ToLower();
            var GroupName = request.GroupDto.GroupName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.SubGroup> Groups = _GroupRepository.Where(x => x.GroupName.Trim().ToLower().Replace(" ", string.Empty) == GroupName && x.GroupId != request.GroupDto.GroupId);



            if (Groups.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.GroupDto.GroupName}' already exists.";

                //response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Group = await _unitOfWork.Repository<Hrm.Domain.SubGroup>().Get(request.GroupDto.GroupId);

                if (Group is null)
                {
                    throw new NotFoundException(nameof(Group), request.GroupDto.GroupId);
                }

                _mapper.Map(request.GroupDto, Group);

                await _unitOfWork.Repository<Hrm.Domain.SubGroup>().Update(Group);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Group.GroupId;

            }

            return response;
        }
    }
}

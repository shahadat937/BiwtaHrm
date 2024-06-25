using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Group.Validators;
using Hrm.Application.Features.Group.Requests.Commands;
using Hrm.Application.Features.Group.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Group.Handlers.Commands
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.SubGroup> _GroupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateGroupCommandHandler(IHrmRepository<Hrm.Domain.SubGroup> GroupRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _GroupRepository = GroupRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateGroupDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.GroupDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var GroupName = request.GroupDto.GroupName.ToLower();

                IQueryable<Hrm.Domain.SubGroup> Groups = _GroupRepository.Where(x => x.GroupName.ToLower() == GroupName);

                if (GroupNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.GroupDto.GroupName}' already exists.";

                    //response.Message = "Creation Failed, Name already exists";
                    response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                }
                else
                {
                    var Group = _mapper.Map<Hrm.Domain.SubGroup>(request.GroupDto);

                    Group = await _unitOfWork.Repository<Hrm.Domain.SubGroup>().Add(Group);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = Group.GroupId;
                }
            }
            return response;
        }
        private bool GroupNameExists(CreateGroupCommand request)
        {
            var GroupName = request.GroupDto.GroupName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.SubGroup> Groups = _GroupRepository.Where(x => x.GroupName.Trim().ToLower().Replace(" ", string.Empty) == GroupName);

            return Groups.Any();
        }
    }
}

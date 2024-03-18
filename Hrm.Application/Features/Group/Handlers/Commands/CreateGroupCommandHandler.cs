using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Group.Validators;
using Hrm.Application.Features.Group.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;


namespace Hrm.Application.Features.Group.Handlers.Commands
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateGroupDtoValidator();
            var validationResult = await validator.ValidateAsync(request.GroupDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Group = _mapper.Map<Hrm.Domain.Group>(request.GroupDto);

                Group = await _unitOfWork.Repository<Hrm.Domain.Group>().Add(Group);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Group.GroupId;
            }

            return response;
        }

    }
}

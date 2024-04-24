using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.UserRole.Validators;
using Hrm.Application.Features.UserRole.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.UserRole.Handlers.Commands
{
    public class CreateUserRoleCommandHandler : IRequestHandler<CreateBloodCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.UserRole> _UserRoleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateUserRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.UserRole> UserRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _UserRoleRepository = UserRoleRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateBloodCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateUserRoleDtoValidator();
            var validationResult = await validator.ValidateAsync(request.UserRoleDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
             //   var UserRoleName = request.UserRoleDto.UserRoleName.Trim().ToLower().Replace(" ", string.Empty);

              //  IQueryable<Hrm.Domain.UserRole> UserRoles = _UserRoleRepository.Where(x => x.UserRoleName.ToLower().Replace(" ", string.Empty) == UserRoleName);


                if (UserRoleNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.UserRoleDto.UserRoleName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                    
                }
                else
                {
                    var UserRole = _mapper.Map<Hrm.Domain.UserRole>(request.UserRoleDto);

                    UserRole = await _unitOfWork.Repository<Hrm.Domain.UserRole>().Add(UserRole);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = UserRole.UserRoleId;
                }
            }

            return response;
        }
        private bool UserRoleNameExists(CreateBloodCommand request)
        {
            var UserRoleName = request.UserRoleDto.UserRoleName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable <Hrm.Domain.UserRole > UserRoles = _UserRoleRepository.Where(x => x.UserRoleName.Trim().ToLower().Replace(" ", string.Empty) == UserRoleName);

             return UserRoles.Any();
        }
    }
}

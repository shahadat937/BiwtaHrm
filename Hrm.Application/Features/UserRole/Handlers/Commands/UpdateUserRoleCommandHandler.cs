using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.UserRole.Validators;
using Hrm.Application.DTOs.GradeType.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.UserRole.Requests.Commands;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.AspNetUserRoles;
using Hrm.Domain;

namespace Hrm.Application.Features.UserRole.Handlers.Commands
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<AspNetUserRoles> _UserRoleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserRoleCommandHandler(IHrmRepository<AspNetUserRoles> UserRoleRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UserRoleRepository = UserRoleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            // Find the existing user role
            var userRole = await _UserRoleRepository.FindOneAsync(x => x.UserId == request.UserRoleDto.UserId && x.RoleId == request.UserRoleDto.OldRoleId);

            if (userRole == null)
            {
                response.Success = false;
                response.Message = "Role not found for the user.";
                return response;
            }

            // Remove the old role
            _unitOfWork.Repository<AspNetUserRoles>().Delete(userRole);

            // Add the new role
            var newUserRole = new AspNetUserRoles
            {
                UserId = request.UserRoleDto.UserId,
                RoleId = request.UserRoleDto.NewRoleId
            };

            await _unitOfWork.Repository<AspNetUserRoles>().Add(newUserRole);

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";

            return response;
        }
    }
}

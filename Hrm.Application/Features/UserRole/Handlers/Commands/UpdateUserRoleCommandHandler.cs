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

namespace Hrm.Application.Features.UserRole.Handlers.Commands
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.UserRole> _UserRoleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserRoleCommandHandler(IHrmRepository<Hrm.Domain.UserRole> UserRoleRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _UserRoleRepository = UserRoleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateUserRoleDtoValidators();
            var validatorResult = await validator.ValidateAsync(request.UserRoleDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            //var UserRoleName = request.UserRoleDto.UserRoleName.ToLower();
            var UserRoleName = request.UserRoleDto.UserRoleName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.UserRole> UserRolees = _UserRoleRepository.Where(x => x.UserRoleName.ToLower() == UserRoleName);

            if (UserRolees.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.UserRoleDto.UserRoleName}' already exists.";

                //response.Message = "Creation Failed, Name already exists";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            else
            {

                var UserRole = await _unitOfWork.Repository<Hrm.Domain.UserRole>().Get(request.UserRoleDto.UserRoleId);

                if (UserRole is null)
                {
                    throw new NotFoundException(nameof(UserRole), request.UserRoleDto.UserRoleId);
                }

                _mapper.Map(request.UserRoleDto, UserRole);

                await _unitOfWork.Repository<Hrm.Domain.UserRole>().Update(UserRole);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = UserRole.UserRoleId;

            }

            return response;
        }
    }
}


using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.UserRole.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.UserRole.Handlers.Commands
{
    public class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUserRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var UserRole = await _unitOfWork.Repository<Hrm.Domain.UserRole>().Get(request.UserRoleId);

            if (UserRole == null)
            {
                throw new NotFoundException(nameof(UserRole), request.UserRoleId);
            }

            await _unitOfWork.Repository<Hrm.Domain.UserRole>().Delete(UserRole);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = UserRole.UserRoleId;

            return response;
        }
    }
}

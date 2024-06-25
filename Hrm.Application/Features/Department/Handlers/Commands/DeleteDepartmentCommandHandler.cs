using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Department.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Department.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Department.Handlers.Commands
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDepartmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Department = await _unitOfWork.Repository<Hrm.Domain.Department>().Get(request.DepartmentId);

            if (Department == null)
            {
                throw new NotFoundException(nameof(Department), request.DepartmentId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Department>().Delete(Department);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Department.DepartmentId;

            return response;
        }
    }
}

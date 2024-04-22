using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SubDepartment.Validators;
using Hrm.Application.Features.SubDepartment.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubDepartment.Handlers.Commands
{
    public class CreateSubDepartmentCommandHandler : IRequestHandler<CreateSubDepartmentCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateSubDepartmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateSubDepartmentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSubDepartmentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SubDepartmentDto);
            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Faild";
                response.Errors=validationResult.Errors.Select(q=>q.ErrorMessage).ToList();
            }
            else
            {
                var SubDepartment = _mapper.Map<Hrm.Domain.SubDepartment>(request.SubDepartmentDto);
                SubDepartment = await _unitOfWork.Repository<Hrm.Domain.SubDepartment>().Add(SubDepartment);
                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = SubDepartment.SubDepartmentId;
            }
            return response;
        }
    }
}

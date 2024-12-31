using AutoMapper;
using FluentValidation;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup.Validators;
using Hrm.Application.DTOs.Department.Validators;
using Hrm.Application.DTOs.DepartmentDepartment.Validators;
using Hrm.Application.Features.Department.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Hrm.Infrastructure.SignalRHub;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Department.Handlers.Commands
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Department> _DepartmentRepository; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _notificationHub;

        public CreateDepartmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Department> DepartmentRepository, IHubContext<NotificationHub> notificationHub)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _DepartmentRepository = DepartmentRepository;
            _notificationHub = notificationHub;
        }


        public async Task<BaseCommandResponse> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDepartmentDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.DepartmentDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x=> x.ErrorMessage).ToList();
            }
            else
            {

                var DepartmentName = request.DepartmentDto.DepartmentName.Trim().ToLower().Replace(" ", string.Empty);

                IQueryable<Hrm.Domain.Department> Departmentes = _DepartmentRepository.Where(x => x.DepartmentName.Trim().ToLower().Replace(" ", string.Empty) == DepartmentName);

                

                //if (Departmentes.Any())
                //{
                //    response.Success = false;
                //    response.Message = "Creation Failed Name already exists.";
                //    response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();

                //}

                //else
                //{
                    var Department = _mapper.Map<Hrm.Domain.Department>(request.DepartmentDto);

                    Department = await _unitOfWork.Repository<Hrm.Domain.Department>().Add(Department);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = Department.DepartmentId;
                //}
            }


            await _notificationHub.Clients.All.SendAsync("Department", response);
            return response;
        }
    }
}

using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Department.Validators;
using Hrm.Application.DTOs.Department.ValidatorsDepartment;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Department.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Department.Handlers.Commands
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Department> _DepartmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDepartmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Department> DepartmentRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _DepartmentRepository = DepartmentRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateDepartmentDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DepartmentDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var DepartmentName = request.DepartmentDto.DepartmentName.ToLower();

            IQueryable<Hrm.Domain.Department> Departmentes = _DepartmentRepository.Where(x => x.DepartmentName.ToLower() == DepartmentName);



            if (Departmentes.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Department = await _unitOfWork.Repository<Hrm.Domain.Department>().Get(request.DepartmentDto.DepartmentId);

                if (Department is null)
                {
                    throw new NotFoundException(nameof(Department), request.DepartmentDto.DepartmentId);
                }

                _mapper.Map(request.DepartmentDto, Department);

                await _unitOfWork.Repository<Hrm.Domain.Department>().Update(Department);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Department.DepartmentId;

            }

            return response;
        }
    }
}

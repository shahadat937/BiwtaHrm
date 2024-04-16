using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmployeeType.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.EmployeeType.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmployeeType.Handlers.Commands
{
    public class UpdateEmployeeTypeCommandHandler : IRequestHandler<UpdateEmployeeTypeCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.EmployeeType> _employeeTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmployeeTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.EmployeeType> employeeTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _employeeTypeRepository = employeeTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmployeeTypeCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateEmployeeTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.EmployeeTypeDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
             
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var EmployeeType = await _unitOfWork.Repository<Hrm.Domain.EmployeeType>().Get(request.EmployeeTypeDto.EmployeeTypeId);

            if (EmployeeType is null)
            {
                throw new NotFoundException(nameof(EmployeeType), request.EmployeeTypeDto.EmployeeTypeId);
            }
            var EmployeeTypeName = request.EmployeeTypeDto.EmployeeTypeName.ToLower();

            IQueryable<Hrm.Domain.EmployeeType> isEmployeeTypeName = _employeeTypeRepository.Where(x => x.EmployeeTypeName.ToLower() == EmployeeTypeName);


            if (isEmployeeTypeName.Any())
            {
                respose.Success = false;
                respose.Message = $"Update Failed '{request.EmployeeTypeDto.EmployeeTypeName}' already exists.";
                respose.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.EmployeeTypeDto, EmployeeType);

                await _unitOfWork.Repository<Hrm.Domain.EmployeeType>().Update(EmployeeType);
                await _unitOfWork.Save();


                respose.Success = true;
                respose.Message = "Update Successful";
                respose.Id = EmployeeType.EmployeeTypeId;

            }

         
            return respose;
        }
    }
}

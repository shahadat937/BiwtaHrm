using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SubDepartment.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.SubDepartment.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubDepartment.Handlers.Commands
{
    public class UpdateSubDepartmentCommandHandler : IRequestHandler<UpdateSubDepartmentCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.SubDepartment> _SubDepartmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateSubDepartmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.SubDepartment> SubDepartmentRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _SubDepartmentRepository = SubDepartmentRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateSubDepartmentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateSubDepartmentDtoValidators();
            var validationResult = await validator.ValidateAsync(request.SubDepartmentDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var SubDepartment = await _unitOfWork.Repository<Hrm.Domain.SubDepartment>().Get(request.SubDepartmentDto.SubDepartmentId);

            if (SubDepartment is null)
            {
                throw new NotFoundException(nameof(SubDepartment), request.SubDepartmentDto.SubDepartmentId);
            }

            //var SubDepartmentName = request.SubDepartmentDto.SubDepartmentName.ToLower();
            var SubDepartmentName = request.SubDepartmentDto.SubDepartmentName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.SubDepartment> SubDepartments = _SubDepartmentRepository.Where(x => x.SubDepartmentName.ToLower() == SubDepartmentName);


            if (SubDepartments.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.SubDepartmentDto.SubDepartmentName}' already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.SubDepartmentDto, SubDepartment);

                await _unitOfWork.Repository<Hrm.Domain.SubDepartment>().Update(SubDepartment);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = SubDepartment.SubDepartmentId;

            }
            return response;
        }
    }
}

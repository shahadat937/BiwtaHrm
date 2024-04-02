using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Designation.Validators;
using Hrm.Application.DTOs.Designation.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Designation.Requests.Commands;
using Hrm.Application.Features.Designation.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designation.Handlers.Commands
{
    public class UpdateDesignationCommandHandler : IRequestHandler<UpdateDesignationCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateDesignationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Designation> DesignationRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _DesignationRepository = DesignationRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateDesignationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new IDesignationDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DesignationDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Designation = await _unitOfWork.Repository<Hrm.Domain.Designation>().Get(request.DesignationDto.DesignationId);

            if (Designation is null)
            {
                throw new NotFoundException(nameof(Designation), request.DesignationDto.DesignationId);
            }

            var DesignationName = request.DesignationDto.DesignationName.ToLower();

            IQueryable<Hrm.Domain.Designation> Designations = _DesignationRepository.Where(x => x.DesignationName.ToLower() == DesignationName);


            if (Designations.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.DesignationDto.DesignationName}' already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.DesignationDto, Designation);

                await _unitOfWork.Repository<Hrm.Domain.Designation>().Update(Designation);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = Designation.DesignationId;

            }
            return response;
        }
    }
}

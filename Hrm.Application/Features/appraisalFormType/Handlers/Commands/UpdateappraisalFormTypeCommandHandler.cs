using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.appraisalFormType.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.appraisalFormType.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.appraisalFormType.Handlers.Commands
{
    public class UpdateappraisalFormTypeCommandHandler : IRequestHandler<UpdateappraisalFormTypeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.appraisalFormType> _appraisalFormTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateappraisalFormTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.appraisalFormType> appraisalFormTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appraisalFormTypeRepository = appraisalFormTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateappraisalFormTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateappraisalFormTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.appraisalFormTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var appraisalFormTypeName = request.appraisalFormTypeDto.appraisalFormTypeName.ToLower();

            IQueryable<Hrm.Domain.appraisalFormType> appraisalFormTypees = _appraisalFormTypeRepository.Where(x => x.appraisalFormTypeName.ToLower() == appraisalFormTypeName);



            if (appraisalFormTypees.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var appraisalFormType = await _unitOfWork.Repository<Hrm.Domain.appraisalFormType>().Get(request.appraisalFormTypeDto.appraisalFormTypeId);

                if (appraisalFormType is null)
                {
                    throw new NotFoundException(nameof(appraisalFormType), request.appraisalFormTypeDto.appraisalFormTypeId);
                }

                _mapper.Map(request.appraisalFormTypeDto, appraisalFormType);

                await _unitOfWork.Repository<Hrm.Domain.appraisalFormType>().Update(appraisalFormType);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = appraisalFormType.appraisalFormTypeId;

            }

            return response;
        }
    }
}

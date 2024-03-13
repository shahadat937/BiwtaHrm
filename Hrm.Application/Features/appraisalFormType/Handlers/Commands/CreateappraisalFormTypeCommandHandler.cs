using AutoMapper;
using FluentValidation;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup.Validators;
using Hrm.Application.DTOs.appraisalFormType.Validators;
using Hrm.Application.Features.appraisalFormType.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.appraisalFormType.Handlers.Commands
{
    public class CreateappraisalFormTypeCommandHandler : IRequestHandler<CreateappraisalFormTypeCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.appraisalFormType> _appraisalFormTypeRepository; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateappraisalFormTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.appraisalFormType> appraisalFormTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _appraisalFormTypeRepository = appraisalFormTypeRepository;
        }


        public async Task<BaseCommandResponse> Handle(CreateappraisalFormTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateappraisalFormTypeDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.appraisalFormTypeDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x=> x.ErrorMessage).ToList();
            }
            else
            {

                var appraisalFormTypeName = request.appraisalFormTypeDto.appraisalFormTypeName.ToLower();

                IQueryable<Hrm.Domain.appraisalFormType> appraisalFormTypees = _appraisalFormTypeRepository.Where(x => x.appraisalFormTypeName.ToLower() == appraisalFormTypeName);

                

                if (appraisalFormTypees.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed Name already exists.";
                    response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();

                }

                else
                {
                    var appraisalFormType = _mapper.Map<Hrm.Domain.appraisalFormType>(request.appraisalFormTypeDto);

                    appraisalFormType = await _unitOfWork.Repository<Hrm.Domain.appraisalFormType>().Add(appraisalFormType);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = appraisalFormType.appraisalFormTypeId;
                }
            }

            return response;
        }
    }
}

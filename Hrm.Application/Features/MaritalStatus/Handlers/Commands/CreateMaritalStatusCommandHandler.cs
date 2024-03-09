using AutoMapper;
using FluentValidation;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup.Validators;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.MaritalStatus.Handlers.Commands
{
    public class CreateMaritalStatusCommandHandler : IRequestHandler<CreateMaritalStatusCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.MaritalStatus> _maritalStatusRepository; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMaritalStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.MaritalStatus> maritalStatusRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _maritalStatusRepository = maritalStatusRepository;
        }


        public async Task<BaseCommandResponse> Handle(CreateMaritalStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateMaritalStatusDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.MaritalStatusDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x=> x.ErrorMessage).ToList();
            }
            else
            {

                var maritalStatusName = request.MaritalStatusDto.MaritalStatusName.ToLower();

                IQueryable<Hrm.Domain.MaritalStatus> maritalStatuses = _maritalStatusRepository.Where(x => x.MaritalStatusName.ToLower() == maritalStatusName);

                

                if (maritalStatuses.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed Name already exists.";
                    response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();

                }

                else
                {
                    var MaritalStatus = _mapper.Map<Hrm.Domain.MaritalStatus>(request.MaritalStatusDto);

                    MaritalStatus = await _unitOfWork.Repository<Hrm.Domain.MaritalStatus>().Add(MaritalStatus);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = MaritalStatus.MaritalStatusId;
                }
            }

            return response;
        }
    }
}

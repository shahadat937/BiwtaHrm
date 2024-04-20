using AutoMapper;
using FluentValidation;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup.Validators;
using Hrm.Application.DTOs.TrainingName.Validators;
using Hrm.Application.Features.TrainingName.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TrainingName.Handlers.Commands
{
    public class CreateTrainingNameCommandHandler : IRequestHandler<CreateTrainingNameCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.TrainingName> _TrainingNameRepository; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTrainingNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.TrainingName> TrainingNameRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _TrainingNameRepository = TrainingNameRepository;
        }


        public async Task<BaseCommandResponse> Handle(CreateTrainingNameCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateTrainingNameDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.TrainingNameDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x=> x.ErrorMessage).ToList();
            }
            else
            {

                var TrainingNameName = request.TrainingNameDto.TrainingNames.ToLower();

                IQueryable<Hrm.Domain.TrainingName> TrainingNamees = _TrainingNameRepository.Where(x => x.TrainingNames.ToLower() == TrainingNameName);

                

                if (TrainingNamees.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed Name already exists.";
                    response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();

                }

                else
                {
                    var TrainingName = _mapper.Map<Hrm.Domain.TrainingName>(request.TrainingNameDto);

                    TrainingName = await _unitOfWork.Repository<Hrm.Domain.TrainingName>().Add(TrainingName);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = TrainingName.TrainingNameId;
                }
            }

            return response;
        }
    }
}

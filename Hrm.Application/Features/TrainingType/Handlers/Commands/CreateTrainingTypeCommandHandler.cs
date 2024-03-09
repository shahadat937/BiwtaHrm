using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.DTOs.TrainingType.Validators;
using Hrm.Application.Features.TrainingType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TrainingType.Handlers.Commands
{
    public class CreateTrainingTypeCommandHandler : IRequestHandler<CreateTrainingTypeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.TrainingType> _trainingTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTrainingTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.TrainingType> trainingTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _trainingTypeRepository = trainingTypeRepository;
        }


        public async Task<BaseCommandResponse> Handle(CreateTrainingTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateTrainingTypeDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.TrainingTypeDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {

                var traineeTypeName = request.TrainingTypeDto.TrainingTypeName.ToLower();

                IQueryable<Hrm.Domain.TrainingType> trainingTypes = _trainingTypeRepository.Where(x => x.TrainingTypeName.ToLower() == traineeTypeName);



                if (trainingTypes.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed Name already exists.";
                    response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();

                }

                else
                {
                    var TraineeType = _mapper.Map<Hrm.Domain.TrainingType>(request.TrainingTypeDto);

                    TraineeType = await _unitOfWork.Repository<Hrm.Domain.TrainingType>().Add(TraineeType);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = TraineeType.TrainingTypeId;
                }
            }

            return response;
        }
    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TrainingName.Validators;
using Hrm.Application.Features.TrainingName.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.TrainingName.Handlers.Commands
{
    public class CreateTrainingNameCommandHandler : IRequestHandler<CreateTrainingNameCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.TrainingName> _trainingNameRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateTrainingNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.TrainingName> trainingNameRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _trainingNameRepository = trainingNameRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateTrainingNameCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateTrainingNameDtoValidator();
            var validationResult = await validator.ValidateAsync(request.TrainingNameDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var trainingNameName = request.TrainingNameDto.TrainingNameName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.TrainingName> trainingNames = _trainingNameRepository.Where(x => x.TrainingNameName.ToLower().Replace(" ", string.Empty) == trainingNameName);


                if (TrainingNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.TrainingNameDto.TrainingNames}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var TrainingName = _mapper.Map<Hrm.Domain.TrainingName>(request.TrainingNameDto);

                    TrainingName = await _unitOfWork.Repository<Hrm.Domain.TrainingName>().Add(TrainingName);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = TrainingName.TrainingNameId;
                }
            }

            return response;
        }
        private bool TrainingNameExists(CreateTrainingNameCommand request)
        {
            var trainingNames = request.TrainingNameDto.TrainingNames.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.TrainingName> trainingName = _trainingNameRepository.Where(x => x.TrainingNames.Trim().ToLower().Replace(" ", string.Empty) == trainingNames);

            return trainingName.Any();
        }
    }
}

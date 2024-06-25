using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.DTOs.TrainingType.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
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
    public class UpdateTrainingTypeCommandHandler : IRequestHandler<UpdateTrainingTypeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.TrainingType> _trainingTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTrainingTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.TrainingType> trainingTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _trainingTypeRepository = trainingTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateTrainingTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateTrainingTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.TrainingTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var trainingTypeName = request.TrainingTypeDto.TrainingTypeName.ToLower();

            IQueryable<Hrm.Domain.TrainingType> trainingTypes = _trainingTypeRepository.Where(x => x.TrainingTypeName.ToLower() == trainingTypeName);



            if (trainingTypes.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var TrainingType = await _unitOfWork.Repository<Hrm.Domain.TrainingType>().Get(request.TrainingTypeDto.TrainingTypeId);

                if (TrainingType is null)
                {
                    throw new NotFoundException(nameof(TrainingType), request.TrainingTypeDto.TrainingTypeId);
                }

                _mapper.Map(request.TrainingTypeDto, TrainingType);

                await _unitOfWork.Repository<Hrm.Domain.TrainingType>().Update(TrainingType);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = TrainingType.TrainingTypeId;

            }

            return response;
        }
    }
}

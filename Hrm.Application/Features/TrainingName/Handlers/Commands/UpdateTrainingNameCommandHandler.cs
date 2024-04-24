using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TrainingName.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.TrainingName.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TrainingName.Handlers.Commands
{
    public class UpdateTrainingNameCommandHandler : IRequestHandler<UpdateTrainingNameCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.TrainingName> _TrainingNameRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTrainingNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.TrainingName> TrainingNameRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _TrainingNameRepository = TrainingNameRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateTrainingNameCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateTrainingNameDtoValidator();
            var validationResult = await validator.ValidateAsync(request.TrainingNameDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var TrainingNameName = request.TrainingNameDto.TrainingNames.ToLower();

            IQueryable<Hrm.Domain.TrainingName> TrainingNamees = _TrainingNameRepository.Where(x => x.TrainingNames.ToLower() == TrainingNameName);



            if (TrainingNamees.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var TrainingName = await _unitOfWork.Repository<Hrm.Domain.TrainingName>().Get(request.TrainingNameDto.TrainingNameId);

                if (TrainingName is null)
                {
                    throw new NotFoundException(nameof(TrainingName), request.TrainingNameDto.TrainingNameId);
                }

                _mapper.Map(request.TrainingNameDto, TrainingName);

                await _unitOfWork.Repository<Hrm.Domain.TrainingName>().Update(TrainingName);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = TrainingName.TrainingNameId;

            }

            return response;
        }
    }
}

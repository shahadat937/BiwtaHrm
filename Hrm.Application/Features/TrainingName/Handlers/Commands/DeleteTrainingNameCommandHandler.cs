using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TrainingName.Validators;
using Hrm.Application.Exceptions;
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
    public class DeleteTrainingNameCommandHandler : IRequestHandler<DeleteTrainingNameCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteTrainingNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteTrainingNameCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var TrainingName = await _unitOfWork.Repository<Hrm.Domain.TrainingName>().Get(request.TrainingNameId);

            if (TrainingName == null)
            {
                throw new NotFoundException(nameof(TrainingName), request.TrainingNameId);
            }

            await _unitOfWork.Repository<Hrm.Domain.TrainingName>().Delete(TrainingName);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = TrainingName.TrainingNameId;

            return response;
        }
    }
}

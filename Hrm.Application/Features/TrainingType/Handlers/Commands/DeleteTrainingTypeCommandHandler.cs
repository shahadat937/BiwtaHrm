using AutoMapper;
using Hrm.Application.Contracts.Persistence;
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
    public class DeleteTrainingTypeCommandHandler : IRequestHandler<DeleteTrainingTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteTrainingTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteTrainingTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var TrainingType = await _unitOfWork.Repository<Hrm.Domain.TrainingType>().Get(request.TrainingTypeId);

            if (TrainingType == null)
            {
                throw new NotFoundException(nameof(TrainingType), request.TrainingTypeId);
            }

            await _unitOfWork.Repository<Hrm.Domain.TrainingType>().Delete(TrainingType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = TrainingType.TrainingTypeId;

            return response;
        }
    }
}
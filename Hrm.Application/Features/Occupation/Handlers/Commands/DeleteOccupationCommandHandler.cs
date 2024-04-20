using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Occupation.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Occupation.Handlers.Commands
{
    public class DeleteOccupationCommandHandler : IRequestHandler<DeleteOccupationCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteOccupationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteOccupationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Occupation = await _unitOfWork.Repository<Hrm.Domain.Occupation>().Get(request.OccupationId);

            if (Occupation == null)
            {
                throw new NotFoundException(nameof(Occupation), request.OccupationId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Occupation>().Delete(Occupation);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Occupation.OccupationId;

            return response;
        }
    }
}

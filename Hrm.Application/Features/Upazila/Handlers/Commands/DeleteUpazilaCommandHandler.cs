using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Upazila.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Upazila.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Upazila.Handlers.Commands
{
    public class DeleteUpazilaCommandHandler : IRequestHandler<DeleteUpazilaCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUpazilaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteUpazilaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Upazila = await _unitOfWork.Repository<Hrm.Domain.Upazila>().Get(request.UpazilaId);

            if (Upazila == null)
            {
                throw new NotFoundException(nameof(Upazila), request.UpazilaId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Upazila>().Delete(Upazila);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Upazila.UpazilaId;

            return response;
        }
    }
}

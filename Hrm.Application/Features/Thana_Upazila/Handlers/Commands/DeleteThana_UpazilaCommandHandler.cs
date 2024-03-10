using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Thana_Upazila.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Thana_Upazila.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana_Upazila.Handlers.Commands
{
    public class DeleteThana_UpazilaCommandHandler : IRequestHandler<DeleteThana_UpazilaCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteThana_UpazilaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteThana_UpazilaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Thana_Upazila = await _unitOfWork.Repository<Hrm.Domain.Thana_Upazila>().Get(request.Thana_UpazilaId);

            if (Thana_Upazila == null)
            {
                throw new NotFoundException(nameof(Thana_Upazila), request.Thana_UpazilaId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Thana_Upazila>().Delete(Thana_Upazila);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Thana_Upazila.Thana_UpazilaId;

            return response;
        }
    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Thana.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Thana.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Thana.Handlers.Commands
{
    public class DeleteThanaCommandHandler : IRequestHandler<DeleteThanaCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteThanaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteThanaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Thana = await _unitOfWork.Repository<Hrm.Domain.Thana>().Get(request.ThanaId);

            if (Thana == null)
            {
                throw new NotFoundException(nameof(Thana), request.ThanaId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Thana>().Delete(Thana);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Thana.ThanaId;

            return response;
        }
    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Division.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Division.Handlers.Commands
{
    public class DeleteDivisionCommandHandler : IRequestHandler<DeleteDivisionCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDivisionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteDivisionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Division = await _unitOfWork.Repository<Hrm.Domain.Division>().Get(request.DivisionId);

            if (Division == null)
            {
                throw new NotFoundException(nameof(Division), request.DivisionId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Division>().Delete(Division);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Division.DivisionId;

            return response;
        }
    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Union.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Union.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Union.Handlers.Commands
{
    public class DeleteUnionCommandHandler : IRequestHandler<DeleteUnionCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUnionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteUnionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Union = await _unitOfWork.Repository<Hrm.Domain.Union>().Get(request.UnionId);

            if (Union == null)
            {
                throw new NotFoundException(nameof(Union), request.UnionId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Union>().Delete(Union);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Union.UnionId;

            return response;
        }
    }
}

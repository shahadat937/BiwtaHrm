using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Relation.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Relation.Handlers.Commands
{
    public class DeleteRelationCommandHandler : IRequestHandler<DeleteRelationCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteRelationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteRelationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Relation = await _unitOfWork.Repository<Hrm.Domain.Relation>().Get(request.RelationId);

            if (Relation == null)
            {
                throw new NotFoundException(nameof(Relation), request.RelationId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Relation>().Delete(Relation);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Relation.RelationId;

            return response;
        }
    }
}

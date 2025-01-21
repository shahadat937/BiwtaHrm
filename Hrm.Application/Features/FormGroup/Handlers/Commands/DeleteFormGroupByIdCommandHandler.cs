using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.FormGroup.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;

namespace Hrm.Application.Features.FormGroup.Handlers.Commands
{
    public class DeleteFormGroupByIdCommandHandler : IRequestHandler<DeleteFormGroupByIdCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFormGroupByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteFormGroupByIdCommand request, CancellationToken cancellationToken)
        {
            var FormGroup = await _unitOfWork.Repository<Hrm.Domain.FormGroup>().Get(request.FormGroupId);

            if(FormGroup == null)
            {
                throw new NotFoundException(nameof(FormGroup), request.FormGroupId);
            }

            await _unitOfWork.Repository<Hrm.Domain.FormGroup>().Delete(FormGroup);
            await _unitOfWork.Save();
            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Deletion Successful";
            response.Id = request.FormGroupId;

            return response;
        }
    }
}

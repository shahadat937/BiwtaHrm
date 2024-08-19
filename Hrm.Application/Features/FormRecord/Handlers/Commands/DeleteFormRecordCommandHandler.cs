using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.FormRecord.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormRecord.Handlers.Commands
{
    public class DeleteFormRecordCommandHandler: IRequestHandler<DeleteFormRecordCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFormRecordCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteFormRecordCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var formRecord = await _unitOfWork.Repository<Hrm.Domain.FormRecord>().Get(request.RecordId);

            if(formRecord == null)
            {
                throw new NotFoundException(nameof(formRecord), request.RecordId);
            }

            await _unitOfWork.Repository<Hrm.Domain.FormRecord>().Delete(formRecord);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = request.RecordId;

            return response;
        }
    }
}

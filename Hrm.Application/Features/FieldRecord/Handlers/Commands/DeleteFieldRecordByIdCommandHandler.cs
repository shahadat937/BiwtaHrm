using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.FieldRecord.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FieldRecord.Handlers.Commands
{
    public class DeleteFieldRecordByIdCommandHandler: IRequestHandler<DeleteFieldRecordByIdCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFieldRecordByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteFieldRecordByIdCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var fieldRecord = await _unitOfWork.Repository<Hrm.Domain.FieldRecord>().Get(request.FieldRecordId);

            if (fieldRecord == null)
            {
                throw new NotFoundException(nameof(fieldRecord),request.FieldRecordId);
            }

            await _unitOfWork.Repository<Hrm.Domain.FieldRecord>().Delete(fieldRecord);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = request.FieldRecordId;

            return response;

        }
    }
}

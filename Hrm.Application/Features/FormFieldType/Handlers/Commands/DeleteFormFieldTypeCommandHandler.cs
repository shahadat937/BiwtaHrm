using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.FormFieldType.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormFieldType.Handlers.Commands
{
    public class DeleteFormFieldTypeCommandHandler: IRequestHandler<DeleteFormFieldTypeCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteFormFieldTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(DeleteFormFieldTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var formFieldType = await _unitOfWork.Repository<Hrm.Domain.FormFieldType>().Get(request.FieldTypeId);

            if(formFieldType==null)
            {
                throw new NotFoundException(nameof(formFieldType), request.FieldTypeId);
            }

            await _unitOfWork.Repository<Hrm.Domain.FormFieldType>().Delete(formFieldType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = request.FieldTypeId;

            return response;

        }
    }
}

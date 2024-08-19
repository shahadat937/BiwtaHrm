using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.FormField.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormField.Handlers.Commands
{
    public class DeleteFormFieldCommandHandler: IRequestHandler<DeleteFormFieldCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFormFieldCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(DeleteFormFieldCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var formField = await _unitOfWork.Repository<Hrm.Domain.FormField>().Get(request.FieldId);

            if (formField == null)
            {
                throw new NotFoundException(nameof(formField), request.FieldId);
            }

            await _unitOfWork.Repository<Hrm.Domain.FormField>().Delete(formField);

            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = request.FieldId;

            return response;

        }
    }
}

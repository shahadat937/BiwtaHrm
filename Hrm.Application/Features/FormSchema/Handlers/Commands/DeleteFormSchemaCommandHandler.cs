using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.FormSchema.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.FormSchema.Handlers.Commands
{
    public class DeleteFormSchemaCommandHandler: IRequestHandler<DeleteFormSchemaCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteFormSchemaCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(DeleteFormSchemaCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var formSchema = await _unitOfWork.Repository<Hrm.Domain.FormSchema>().Get(request.SchemaId);

            if(formSchema == null)
            {
                throw new NotFoundException(nameof(formSchema), request.SchemaId);
            }

            await _unitOfWork.Repository<Hrm.Domain.FormSchema>().Delete(formSchema);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = request.SchemaId;

            return response;
        }
    }
}

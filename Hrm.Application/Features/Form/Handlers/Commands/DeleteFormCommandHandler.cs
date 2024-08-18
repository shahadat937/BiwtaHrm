using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Form.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Form.Handlers.Commands
{
    public class DeleteFormCommandHandler: IRequestHandler<DeleteFormCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteFormCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(DeleteFormCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var form = await _unitOfWork.Repository<Hrm.Domain.Form>().Get(request.FormId);

            if(form==null)
            {
                throw new NotFoundException(nameof(form), request.FormId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Form>().Delete(form);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = request.FormId;

            return response;
            
        }
    }
}

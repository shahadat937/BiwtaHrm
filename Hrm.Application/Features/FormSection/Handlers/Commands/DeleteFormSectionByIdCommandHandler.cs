using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Union;
using Hrm.Application.Features.FormSection.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;

namespace Hrm.Application.Features.FormSection.Handlers.Commands
{
    public class DeleteFormSectionByIdCommandHandler : IRequestHandler<DeleteFormSectionByIdCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteFormSectionByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteFormSectionByIdCommand request, CancellationToken cancellationToken)
        {
            var FormSection = await _unitOfWork.Repository<Hrm.Domain.FormSection>().Get(request.FormSectionId);

            if (FormSection == null)
            {
                throw new NotFoundException(nameof(FormSection), request.FormSectionId);
            }

            await _unitOfWork.Repository<Hrm.Domain.FormSection>().Delete(FormSection);
            await _unitOfWork.Save();

            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Deletion Successful";
            response.Id = request.FormSectionId;

            return response;
        }
    }
}

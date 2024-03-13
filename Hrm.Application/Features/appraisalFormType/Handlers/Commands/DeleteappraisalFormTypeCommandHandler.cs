using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.appraisalFormType.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.appraisalFormType.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.appraisalFormType.Handlers.Commands
{
    public class DeleteappraisalFormTypeCommandHandler : IRequestHandler<DeleteappraisalFormTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteappraisalFormTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteappraisalFormTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var appraisalFormType = await _unitOfWork.Repository<Hrm.Domain.appraisalFormType>().Get(request.appraisalFormTypeId);

            if (appraisalFormType == null)
            {
                throw new NotFoundException(nameof(appraisalFormType), request.appraisalFormTypeId);
            }

            await _unitOfWork.Repository<Hrm.Domain.appraisalFormType>().Delete(appraisalFormType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = appraisalFormType.appraisalFormTypeId;

            return response;
        }
    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.DepReleaseInfo.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DepReleaseInfo.Handlers.Commands
{
    public class DeleteDepReleaseInfoCommandHandler : IRequestHandler<DeleteDepReleaseInfoCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDepReleaseInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteDepReleaseInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var DepReleaseInfo = await _unitOfWork.Repository<Hrm.Domain.DepReleaseInfo>().Get(request.DepReleaseInfoId);

            if (DepReleaseInfo == null)
            {
                throw new NotFoundException(nameof(DepReleaseInfo), request.DepReleaseInfoId);
            }

            await _unitOfWork.Repository<Hrm.Domain.DepReleaseInfo>().Delete(DepReleaseInfo);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = DepReleaseInfo.DepReleaseInfoId;

            return response;
        }
    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.DesignationSetups.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DesignationSetups.Handlers.Commands
{
    public class DeleteDesignationSetupCommandHandler : IRequestHandler<DeleteDesignationSetupCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDesignationSetupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteDesignationSetupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var DesignationSetup = await _unitOfWork.Repository<Hrm.Domain.DesignationSetup>().Get(request.DesignationSetupId);

            if (DesignationSetup == null)
            {
                throw new NotFoundException(nameof(DesignationSetup), request.DesignationSetupId);
            }

            await _unitOfWork.Repository<Hrm.Domain.DesignationSetup>().Delete(DesignationSetup);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = DesignationSetup.Id;

            return response;
        }
    }
}

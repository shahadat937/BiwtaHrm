using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Department.Requests.Commands;
using Hrm.Application.Features.EmpOtherResponsibilities.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpOtherResponsibilities.Handlers.Commands
{
    public class DeleteEmpOtherResponsibilityCommandHandler : IRequestHandler<DeleteEmpOtherResponsibilityCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmpOtherResponsibilityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmpOtherResponsibilityCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var EmpOtherResponsibility = await _unitOfWork.Repository<Hrm.Domain.EmpOtherResponsibility>().Get(request.Id);

            if (EmpOtherResponsibility == null)
            {
                throw new NotFoundException(nameof(EmpOtherResponsibility), request.Id);
            }

            await _unitOfWork.Repository<Hrm.Domain.EmpOtherResponsibility>().Delete(EmpOtherResponsibility);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = EmpOtherResponsibility.Id;

            return response;
        }
    }
}

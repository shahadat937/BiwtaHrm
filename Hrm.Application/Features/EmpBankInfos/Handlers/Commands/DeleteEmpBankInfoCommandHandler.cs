using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Department.Requests.Commands;
using Hrm.Application.Features.EmpBankInfos.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBankInfos.Handlers.Commands
{
    public class DeleteEmpBankInfoCommandHandler : IRequestHandler<DeleteEmpBankInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmpBankInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmpBankInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var EmpBankInfo = await _unitOfWork.Repository<Hrm.Domain.EmpBankInfo>().Get(request.Id);

            if (EmpBankInfo == null)
            {
                throw new NotFoundException(nameof(EmpBankInfo), request.Id);
            }

            await _unitOfWork.Repository<Hrm.Domain.EmpBankInfo>().Delete(EmpBankInfo);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = EmpBankInfo.Id;

            return response;
        }
    }
}

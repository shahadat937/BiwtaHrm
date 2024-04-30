using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.EmpTnsferPostingJoin.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTnsferPostingJoin.Handlers.Commands
{
    public class DeleteEmpTnsferPostingJoinCommandHandler : IRequestHandler<DeleteEmpTnsferPostingJoinCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmpTnsferPostingJoinCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmpTnsferPostingJoinCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var EmpTnsferPostingJoin = await _unitOfWork.Repository<Hrm.Domain.EmpTnsferPostingJoin>().Get(request.EmpTnsferPostingJoinId);

            if (EmpTnsferPostingJoin == null)
            {
                throw new NotFoundException(nameof(EmpTnsferPostingJoin), request.EmpTnsferPostingJoinId);
            }

            await _unitOfWork.Repository<Hrm.Domain.EmpTnsferPostingJoin>().Delete(EmpTnsferPostingJoin);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = EmpTnsferPostingJoin.EmpTnsferPostingJoinId;

            return response;
        }
    }
}

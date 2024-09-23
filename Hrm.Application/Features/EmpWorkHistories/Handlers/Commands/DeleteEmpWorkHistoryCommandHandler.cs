using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Department.Requests.Commands;
using Hrm.Application.Features.EmpWorkHistories.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpWorkHistories.Handlers.Commands
{
    public class DeleteEmpWorkHistoryCommandHandler : IRequestHandler<DeleteEmpWorkHistoryCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmpWorkHistoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmpWorkHistoryCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var EmpWorkHistory = await _unitOfWork.Repository<Hrm.Domain.EmpWorkHistory>().Get(request.Id);

            if (EmpWorkHistory == null)
            {
                throw new NotFoundException(nameof(EmpWorkHistory), request.Id);
            }

            await _unitOfWork.Repository<Hrm.Domain.EmpWorkHistory>().Delete(EmpWorkHistory);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = EmpWorkHistory.Id;

            return response;
        }
    }
}

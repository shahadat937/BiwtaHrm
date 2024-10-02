using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.EmpRewardPunishments.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpRewardPunishments.Handlers.Commands
{
    public class DeleteEmpRewardPunishmentCommandHandler : IRequestHandler<DeleteEmpRewardPunishmentCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmpRewardPunishmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmpRewardPunishmentCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var EmpRewardPunishment = await _unitOfWork.Repository<Hrm.Domain.EmpRewardPunishment>().Get(request.Id);

            if (EmpRewardPunishment == null)
            {
                throw new NotFoundException(nameof(EmpRewardPunishment), request.Id);
            }

            await _unitOfWork.Repository<Hrm.Domain.EmpRewardPunishment>().Delete(EmpRewardPunishment);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = EmpRewardPunishment.Id;

            return response;
        }
    }
}

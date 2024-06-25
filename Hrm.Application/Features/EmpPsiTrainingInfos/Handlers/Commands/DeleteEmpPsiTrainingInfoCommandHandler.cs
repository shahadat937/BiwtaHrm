using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Department.Requests.Commands;
using Hrm.Application.Features.EmpPsiTrainingInfos.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpPsiTrainingInfos.Handlers.Commands
{
    public class DeleteEmpPsiTrainingInfoCommandHandler : IRequestHandler<DeleteEmpPsiTrainingInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmpPsiTrainingInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmpPsiTrainingInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var EmpPsiTrainingInfo = await _unitOfWork.Repository<Hrm.Domain.EmpPsiTrainingInfo>().Get(request.Id);

            if (EmpPsiTrainingInfo == null)
            {
                throw new NotFoundException(nameof(EmpPsiTrainingInfo), request.Id);
            }

            await _unitOfWork.Repository<Hrm.Domain.EmpPsiTrainingInfo>().Delete(EmpPsiTrainingInfo);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = EmpPsiTrainingInfo.Id;

            return response;
        }
    }
}

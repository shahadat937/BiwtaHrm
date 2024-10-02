using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Department.Requests.Commands;
using Hrm.Application.Features.EmpTrainingInfos.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTrainingInfos.Handlers.Commands
{
    public class DeleteEmpTrainingInfoCommandHandler : IRequestHandler<DeleteEmpTrainingInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmpTrainingInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmpTrainingInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var EmpTrainingInfo = await _unitOfWork.Repository<Hrm.Domain.EmpTrainingInfo>().Get(request.Id);

            if (EmpTrainingInfo == null)
            {
                throw new NotFoundException(nameof(EmpTrainingInfo), request.Id);
            }

            await _unitOfWork.Repository<Hrm.Domain.EmpTrainingInfo>().Delete(EmpTrainingInfo);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = EmpTrainingInfo.Id;

            return response;
        }
    }
}

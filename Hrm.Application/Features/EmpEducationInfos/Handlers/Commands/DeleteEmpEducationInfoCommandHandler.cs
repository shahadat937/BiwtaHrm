using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Department.Requests.Commands;
using Hrm.Application.Features.EmpEducationInfos.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpEducationInfos.Handlers.Commands
{
    public class DeleteEmpEducationInfoCommandHandler : IRequestHandler<DeleteEmpEducationInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmpEducationInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmpEducationInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var EmpEducationInfo = await _unitOfWork.Repository<Hrm.Domain.EmpEducationInfo>().Get(request.Id);

            if (EmpEducationInfo == null)
            {
                throw new NotFoundException(nameof(EmpEducationInfo), request.Id);
            }

            await _unitOfWork.Repository<Hrm.Domain.EmpEducationInfo>().Delete(EmpEducationInfo);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = EmpEducationInfo.Id;

            return response;
        }
    }
}

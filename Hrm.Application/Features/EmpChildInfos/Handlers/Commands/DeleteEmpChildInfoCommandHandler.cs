using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Department.Requests.Commands;
using Hrm.Application.Features.EmpChildInfos.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpChildInfos.Handlers.Commands
{
    public class DeleteEmpChildInfoCommandHandler : IRequestHandler<DeleteEmpChildInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmpChildInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmpChildInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var EmpChildInfo = await _unitOfWork.Repository<Hrm.Domain.EmpChildInfo>().Get(request.Id);

            if (EmpChildInfo == null)
            {
                throw new NotFoundException(nameof(EmpChildInfo), request.Id);
            }

            await _unitOfWork.Repository<Hrm.Domain.EmpChildInfo>().Delete(EmpChildInfo);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = EmpChildInfo.Id;

            return response;
        }
    }
}

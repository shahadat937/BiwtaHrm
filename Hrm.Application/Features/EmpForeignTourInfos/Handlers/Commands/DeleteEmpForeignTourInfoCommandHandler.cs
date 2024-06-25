using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Department.Requests.Commands;
using Hrm.Application.Features.EmpForeignTourInfos.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpForeignTourInfos.Handlers.Commands
{
    public class DeleteEmpForeignTourInfoCommandHandler : IRequestHandler<DeleteEmpForeignTourInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmpForeignTourInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmpForeignTourInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var EmpForeignTourInfo = await _unitOfWork.Repository<Hrm.Domain.EmpForeignTourInfo>().Get(request.Id);

            if (EmpForeignTourInfo == null)
            {
                throw new NotFoundException(nameof(EmpForeignTourInfo), request.Id);
            }

            await _unitOfWork.Repository<Hrm.Domain.EmpForeignTourInfo>().Delete(EmpForeignTourInfo);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = EmpForeignTourInfo.Id;

            return response;
        }
    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Department.Requests.Commands;
using Hrm.Application.Features.EmpSpouseInfos.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpSpouseInfos.Handlers.Commands
{
    public class DeleteEmpSpouseInfoCommandHandler : IRequestHandler<DeleteEmpSpouseInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmpSpouseInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmpSpouseInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var EmpSpouseInfo = await _unitOfWork.Repository<Hrm.Domain.EmpSpouseInfo>().Get(request.Id);

            if (EmpSpouseInfo == null)
            {
                throw new NotFoundException(nameof(EmpSpouseInfo), request.Id);
            }

            await _unitOfWork.Repository<Hrm.Domain.EmpSpouseInfo>().Delete(EmpSpouseInfo);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = EmpSpouseInfo.Id;

            return response;
        }
    }
}

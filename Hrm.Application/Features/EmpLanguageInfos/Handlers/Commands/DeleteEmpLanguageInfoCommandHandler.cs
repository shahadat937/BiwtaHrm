using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Department.Requests.Commands;
using Hrm.Application.Features.EmpLanguageInfos.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpLanguageInfos.Handlers.Commands
{
    public class DeleteEmpLanguageInfoCommandHandler : IRequestHandler<DeleteEmpLanguageInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmpLanguageInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteEmpLanguageInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var EmpLanguageInfo = await _unitOfWork.Repository<Hrm.Domain.EmpLanguageInfo>().Get(request.Id);

            if (EmpLanguageInfo == null)
            {
                throw new NotFoundException(nameof(EmpLanguageInfo), request.Id);
            }

            await _unitOfWork.Repository<Hrm.Domain.EmpLanguageInfo>().Delete(EmpLanguageInfo);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = EmpLanguageInfo.Id;

            return response;
        }
    }
}

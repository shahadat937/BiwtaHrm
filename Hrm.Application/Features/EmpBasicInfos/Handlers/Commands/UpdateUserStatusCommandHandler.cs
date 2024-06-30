using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpBasicInfos.Requests.Commands;
using Hrm.Application.Features.EmpBasicInfos.Requests.Queries;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpBasicInfos.Handlers.Commands
{
    public class UpdateUserStatusCommandHandler : IRequestHandler<UpdateUserStatusCommand, object>
    {

        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
        }

        public async Task<object> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var EmpBasicInfos = await _unitOfWork.Repository<EmpBasicInfo>().Get(request.Id);
            EmpBasicInfos.UserStatus = true;

            await _unitOfWork.Repository<EmpBasicInfo>().Update(EmpBasicInfos);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";
            response.Id = EmpBasicInfos.Id;

            return response;
        }
    }
}
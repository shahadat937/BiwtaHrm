//using AutoMapper;
//using Hrm.Application.Contracts.Persistence;
//using Hrm.Application.DTOs.DepReleaseInfo.Validators;
//using Hrm.Application.Features.DepReleaseInfo.Requests.Commands;
//using Hrm.Application.Responses;
//using MediatR;
//using Hrm.Domain;

//namespace Hrm.Application.Features.DepReleaseInfo.Handlers.Commands
//{
//    public class CreateDepReleaseInfoApprovedCommandHandler : IRequestHandler<CreateDepReleaseInfoCommand, BaseCommandResponse>
//    {
//        private readonly IHrmRepository<Hrm.Domain.TransferApproveInfo> _TransferApproveInfoRepository;
//        private readonly IUnitOfWork _unitOfWork;
//        private readonly IMapper _mapper;
//        public CreateDepReleaseInfoApprovedCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.TransferApproveInfo> TransferApproveInfoRepository,)
//        {
//            _TransferApproveInfoRepository = TransferApproveInfoRepository;
//            _unitOfWork = unitOfWork;
//            _mapper = mapper;
//        }
//        public async Task<BaseCommandResponse> Handle(CreateDepReleaseInfoCommand request, CancellationToken cancellationToken)
//        {
//            IQueryable<Hrm.Domain.TransferApproveInfo> TransferApproveInfos = _TransferApproveInfoRepository.FilterWithInclude(x => true);
//            TransferApproveInfos = TransferApproveInfos.OrderByDescending(x => x.ApproveStatus==false);
            
//            var response = new BaseCommandResponse();
//            var validator = new CreateDepReleaseInfoDtoValidator();
//            var validationResult = await validator.ValidateAsync(request.DepReleaseInfoDto);
            

//            if (validationResult.IsValid == false)
//            {
//                response.Success = false;
//                response.Message = "Creation Failed";
//                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
//            }
//            else
//            {
//                var DepReleaseInfo = _mapper.Map<Hrm.Domain.DepReleaseInfo>(request.DepReleaseInfoDto); 

//                DepReleaseInfo = await _unitOfWork.Repository<Hrm.Domain.DepReleaseInfo>().Add(DepReleaseInfo);
//                await _unitOfWork.Save();


//                response.Success = true;
//                response.Message = "Creation Successful";
//                response.Id = DepReleaseInfo.DepReleaseInfoId;
//            }

//            return response;
//        }

//    }
//}



using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.DepReleaseInfo.Validators;
using Hrm.Application.Features.DepReleaseInfo.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using System.Linq;

namespace Hrm.Application.Features.DepReleaseInfo.Handlers.Commands
{
    public class CreateDepReleaseInfoApprovedCommandHandler : IRequestHandler<CreateDepReleaseInfoCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.TransferApproveInfo> _TransferApproveInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDepReleaseInfoApprovedCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.TransferApproveInfo> TransferApproveInfoRepository)
        {
            _TransferApproveInfoRepository = TransferApproveInfoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDepReleaseInfoCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.TransferApproveInfo> TransferApproveInfos = _TransferApproveInfoRepository.FilterWithInclude(x => true);
            var mostRecentTransferApproveInfo = TransferApproveInfos.OrderByDescending(x => x.TransferApproveInfoId).FirstOrDefault();

            var response = new BaseCommandResponse();
            var validator = new CreateDepReleaseInfoDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DepReleaseInfoDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
           
            else
            {
                var DepReleaseInfo = _mapper.Map<Hrm.Domain.DepReleaseInfo>(request.DepReleaseInfoDto);
                DepReleaseInfo = await _unitOfWork.Repository<Hrm.Domain.DepReleaseInfo>().Add(DepReleaseInfo);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = DepReleaseInfo.DepReleaseInfoId;
            }

            return response;
        }
    }
}


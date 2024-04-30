using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TransferApproveInfo.Validators;
using Hrm.Application.Features.TransferApproveInfo.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;

namespace Hrm.Application.Features.TransferApproveInfo.Handlers.Commands
{
    public class CreateTransferApproveInfoCommandHandler : IRequestHandler<CreateTransferApproveInfoCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.TransferApproveInfo> _TransferApproveInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateTransferApproveInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.TransferApproveInfo> TransferApproveInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _TransferApproveInfoRepository = TransferApproveInfoRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateTransferApproveInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateTransferApproveInfoDtoValidator();
            var validationResult = await validator.ValidateAsync(request.TransferApproveInfoDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var TransferApproveInfoName = request.TransferApproveInfoDto.TransferApproveInfoName.ToLower();

                IQueryable<Hrm.Domain.TransferApproveInfo> TransferApproveInfos = _TransferApproveInfoRepository.Where(x => x.TransferApproveInfoName.ToLower() == TransferApproveInfoName);


                if (TransferApproveInfoNameExists(request))
                {
                    response.Success = false;
                    // response.Message = "Creation Failed Name already exists.";
                    response.Message = $"Creation Failed '{request.TransferApproveInfoDto.TransferApproveInfoName}' already exists.";

                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var TransferApproveInfo = _mapper.Map<Hrm.Domain.TransferApproveInfo>(request.TransferApproveInfoDto);

                    TransferApproveInfo = await _unitOfWork.Repository<Hrm.Domain.TransferApproveInfo>().Add(TransferApproveInfo);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = TransferApproveInfo.TransferApproveInfoId;
                }
            }

            return response;
        }
        private bool TransferApproveInfoNameExists(CreateTransferApproveInfoCommand request)
        {
            var TransferApproveInfoName = request.TransferApproveInfoDto.TransferApproveInfoName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.TransferApproveInfo> TransferApproveInfos = _TransferApproveInfoRepository.Where(x => x.TransferApproveInfoName.Trim().ToLower().Replace(" ", string.Empty) == TransferApproveInfoName);

            return TransferApproveInfos.Any();
        }

    }
}

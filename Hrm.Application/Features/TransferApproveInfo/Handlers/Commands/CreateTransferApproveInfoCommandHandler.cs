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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateTransferApproveInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                var TransferApproveInfo = _mapper.Map<Hrm.Domain.TransferApproveInfo>(request.TransferApproveInfoDto);

                TransferApproveInfo = await _unitOfWork.Repository<Hrm.Domain.TransferApproveInfo>().Add(TransferApproveInfo);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = TransferApproveInfo.TransferApproveInfoId;
            }

            return response;
        }

    }
}

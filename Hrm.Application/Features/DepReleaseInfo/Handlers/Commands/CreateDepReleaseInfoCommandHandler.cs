using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.DepReleaseInfo.Validators;
using Hrm.Application.Features.DepReleaseInfo.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;

namespace Hrm.Application.Features.DepReleaseInfo.Handlers.Commands
{
    public class CreateDepReleaseInfoCommandHandler : IRequestHandler<CreateDepReleaseInfoCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateDepReleaseInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateDepReleaseInfoCommand request, CancellationToken cancellationToken)
        {
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

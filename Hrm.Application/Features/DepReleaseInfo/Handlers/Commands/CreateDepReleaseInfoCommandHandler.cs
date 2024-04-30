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
        private readonly IHrmRepository<Hrm.Domain.DepReleaseInfo> _DepReleaseInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateDepReleaseInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.DepReleaseInfo> DepReleaseInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _DepReleaseInfoRepository = DepReleaseInfoRepository;
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
                var DepReleaseInfoName = request.DepReleaseInfoDto.DepReleaseInfoName.ToLower();

                IQueryable<Hrm.Domain.DepReleaseInfo> DepReleaseInfos = _DepReleaseInfoRepository.Where(x => x.DepReleaseInfoName.ToLower() == DepReleaseInfoName);


                if (DepReleaseInfoNameExists(request))
                {
                    response.Success = false;
                    // response.Message = "Creation Failed Name already exists.";
                    response.Message = $"Creation Failed '{request.DepReleaseInfoDto.DepReleaseInfoName}' already exists.";

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
            }

            return response;
        }
        private bool DepReleaseInfoNameExists(CreateDepReleaseInfoCommand request)
        {
            var DepReleaseInfoName = request.DepReleaseInfoDto.DepReleaseInfoName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.DepReleaseInfo> DepReleaseInfos = _DepReleaseInfoRepository.Where(x => x.DepReleaseInfoName.Trim().ToLower().Replace(" ", string.Empty) == DepReleaseInfoName);

            return DepReleaseInfos.Any();
        }

    }
}

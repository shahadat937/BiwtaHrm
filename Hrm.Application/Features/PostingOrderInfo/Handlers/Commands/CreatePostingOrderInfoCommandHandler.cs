using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.PostingOrderInfo.Validators;
using Hrm.Application.Features.PostingOrderInfo.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;

namespace Hrm.Application.Features.PostingOrderInfo.Handlers.Commands
{
    public class CreatePostingOrderInfoCommandHandler : IRequestHandler<CreatePostingOrderInfoCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.PostingOrderInfo> _PostingOrderInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePostingOrderInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.PostingOrderInfo> PostingOrderInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _PostingOrderInfoRepository = PostingOrderInfoRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreatePostingOrderInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreatePostingOrderInfoDtoValidator();
            var validationResult = await validator.ValidateAsync(request.PostingOrderInfoDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var PostingOrderInfoName = request.PostingOrderInfoDto.PostingOrderInfoName.ToLower();

                IQueryable<Hrm.Domain.PostingOrderInfo> PostingOrderInfos = _PostingOrderInfoRepository.Where(x => x.PostingOrderInfoName.ToLower() == PostingOrderInfoName);


                if (PostingOrderInfoNameExists(request))
                {
                    response.Success = false;
                    // response.Message = "Creation Failed Name already exists.";
                    response.Message = $"Creation Failed '{request.PostingOrderInfoDto.PostingOrderInfoName}' already exists.";

                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var PostingOrderInfo = _mapper.Map<Hrm.Domain.PostingOrderInfo>(request.PostingOrderInfoDto);

                    PostingOrderInfo = await _unitOfWork.Repository<Hrm.Domain.PostingOrderInfo>().Add(PostingOrderInfo);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = PostingOrderInfo.PostingOrderInfoId;
                }
            }

            return response;
        }
        private bool PostingOrderInfoNameExists(CreatePostingOrderInfoCommand request)
        {
            var PostingOrderInfoName = request.PostingOrderInfoDto.PostingOrderInfoName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.PostingOrderInfo> PostingOrderInfos = _PostingOrderInfoRepository.Where(x => x.PostingOrderInfoName.Trim().ToLower().Replace(" ", string.Empty) == PostingOrderInfoName);

            return PostingOrderInfos.Any();
        }

    }
}

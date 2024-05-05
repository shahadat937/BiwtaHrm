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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePostingOrderInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                var PostingOrderInfo = _mapper.Map<Hrm.Domain.PostingOrderInfo>(request.PostingOrderInfoDto);

                PostingOrderInfo = await _unitOfWork.Repository<Hrm.Domain.PostingOrderInfo>().Add(PostingOrderInfo);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = PostingOrderInfo.PostingOrderInfoId;
            }

            return response;
        }

    }
}

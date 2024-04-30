using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.PostingOrderInfo.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.PostingOrderInfo.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PostingOrderInfo.Handlers.Commands
{
    public class UpdatePostingOrderInfoCommandHandler : IRequestHandler<UpdatePostingOrderInfoCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.PostingOrderInfo> _PostingOrderInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdatePostingOrderInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.PostingOrderInfo> PostingOrderInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _PostingOrderInfoRepository = PostingOrderInfoRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdatePostingOrderInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdatePostingOrderInfoDtoValidators();
            var validationResult = await validator.ValidateAsync(request.PostingOrderInfoDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var PostingOrderInfo = await _unitOfWork.Repository<Hrm.Domain.PostingOrderInfo>().Get(request.PostingOrderInfoDto.PostingOrderInfoId);

            if (PostingOrderInfo is null)
            {
                throw new NotFoundException(nameof(PostingOrderInfo), request.PostingOrderInfoDto.PostingOrderInfoId);
            }

            //var PostingOrderInfoName = request.PostingOrderInfoDto.PostingOrderInfoName.ToLower();
            var PostingOrderInfoName = request.PostingOrderInfoDto.PostingOrderInfoName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.PostingOrderInfo> PostingOrderInfos = _PostingOrderInfoRepository.Where(x => x.PostingOrderInfoName.ToLower() == PostingOrderInfoName);


            if (PostingOrderInfos.Any())
            {
                response.Success = false;
                // response.Message = "Creation Failed Name already exists.";
                response.Message = $"Creation Failed '{request.PostingOrderInfoDto.PostingOrderInfoName}' already exists.";

                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.PostingOrderInfoDto, PostingOrderInfo);

                await _unitOfWork.Repository<Hrm.Domain.PostingOrderInfo>().Update(PostingOrderInfo);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = PostingOrderInfo.PostingOrderInfoId;

            }
            return response;
        }
    }
}

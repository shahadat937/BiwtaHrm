using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.DepReleaseInfo.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.DepReleaseInfo.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DepReleaseInfo.Handlers.Commands
{
    public class UpdateDepReleaseInfoCommandHandler : IRequestHandler<UpdateDepReleaseInfoCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.DepReleaseInfo> _DepReleaseInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateDepReleaseInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.DepReleaseInfo> DepReleaseInfoRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _DepReleaseInfoRepository = DepReleaseInfoRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateDepReleaseInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateDepReleaseInfoDtoValidators();
            var validationResult = await validator.ValidateAsync(request.DepReleaseInfoDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var DepReleaseInfo = await _unitOfWork.Repository<Hrm.Domain.DepReleaseInfo>().Get(request.DepReleaseInfoDto.DepReleaseInfoId);

            if (DepReleaseInfo is null)
            {
                throw new NotFoundException(nameof(DepReleaseInfo), request.DepReleaseInfoDto.DepReleaseInfoId);
            }

            //var DepReleaseInfoName = request.DepReleaseInfoDto.DepReleaseInfoName.ToLower();
            var DepReleaseInfoName = request.DepReleaseInfoDto.DepReleaseInfoName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.DepReleaseInfo> DepReleaseInfos = _DepReleaseInfoRepository.Where(x => x.DepReleaseInfoName.ToLower() == DepReleaseInfoName);


            if (DepReleaseInfos.Any())
            {
                response.Success = false;
                // response.Message = "Creation Failed Name already exists.";
                response.Message = $"Creation Failed '{request.DepReleaseInfoDto.DepReleaseInfoName}' already exists.";

                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.DepReleaseInfoDto, DepReleaseInfo);

                await _unitOfWork.Repository<Hrm.Domain.DepReleaseInfo>().Update(DepReleaseInfo);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = DepReleaseInfo.DepReleaseInfoId;

            }
            return response;
        }
    }
}

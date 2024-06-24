using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.DepReleaseInfo.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.DepReleaseInfo.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DepReleaseInfo.Handlers.Commands
{
    public class UpdateDepReleaseInfoCommandHandler : IRequestHandler<UpdateDepReleaseInfoCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDepReleaseInfoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                response.Success = false;
                response.Message = "Creation Failed";
            }

            _mapper.Map(request.DepReleaseInfoDto, DepReleaseInfo);

            await _unitOfWork.Repository<Hrm.Domain.DepReleaseInfo>().Update(DepReleaseInfo);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";
            response.Id = DepReleaseInfo.DepReleaseInfoId;

            return response;
        }
    }
}

using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.ReleaseTypes.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ReleaseTypes.Handlers.Commands
{
    public class UpdateReleaseTypeCommandHandler : IRequestHandler<UpdateReleaseTypeCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateReleaseTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateReleaseTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var ReleaseType = await _unitOfWork.Repository<Hrm.Domain.ReleaseType>().Get(request.ReleaseTypeDto.ReleaseTypeId);

            if (ReleaseType is null)
            {
                response.Success = false;
                response.Message = "Update Failed";
            }

            _mapper.Map(request.ReleaseTypeDto, ReleaseType);

            await _unitOfWork.Repository<Hrm.Domain.ReleaseType>().Update(ReleaseType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";
            response.Id = ReleaseType.ReleaseTypeId;

            return response;
        }
    }
}

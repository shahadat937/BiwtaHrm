using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Scale.Validators;
using Hrm.Application.Features.Scales.Requests.Commands;
using Hrm.Application.Features.Scales.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Scales.Handlers.Commands
{

    public class CreateScaleCommandHandler : IRequestHandler<CreateScaleCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateScaleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateScaleCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateScaleDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ScaleDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Scale = _mapper.Map<Hrm.Domain.Scale>(request.ScaleDto);

                Scale = await _unitOfWork.Repository<Hrm.Domain.Scale>().Add(Scale);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Scale.ScaleId;
            }

            return response;
        }

    }
}

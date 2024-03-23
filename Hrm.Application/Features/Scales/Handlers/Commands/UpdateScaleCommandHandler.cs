using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Scale.Validators;
using Hrm.Application.Exceptions;
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

    public class UpdateScaleCommandHandler : IRequestHandler<UpdateScaleCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateScaleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateScaleCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateScaleDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ScaleDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Scale = await _unitOfWork.Repository<Hrm.Domain.Scale>().Get(request.ScaleDto.ScaleId);

            if (Scale is null)
            {
                throw new NotFoundException(nameof(Scale), request.ScaleDto.ScaleId);
            }

            _mapper.Map(request.ScaleDto, Scale);

            await _unitOfWork.Repository<Hrm.Domain.Scale>().Update(Scale);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

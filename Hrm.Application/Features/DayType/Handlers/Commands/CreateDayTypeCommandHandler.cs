using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.DayType.Validators;
using Hrm.Application.Features.DayType.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DayType.Handlers.Commands
{
    public class CreateDayTypeCommandHandler : IRequestHandler<CreateDayTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateDayTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateDayTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDayTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DayTypeDto);

            if(validationResult.IsValid==false)
            {
                throw new ValidationException(validationResult);
            }

            var Daytype = _mapper.Map<Hrm.Domain.DayType>(request.DayTypeDto);

            Daytype = await _unitOfWork.Repository<Hrm.Domain.DayType>().Add(Daytype);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = Daytype.DayTypeId;

            return response;
        }
    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SelectableOption.Validators;
using Hrm.Application.Features.SelectableOption.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SelectableOption.Handlers.Commands
{
    public class CreateOptionCommandHandler: IRequestHandler<CreateOptionCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOptionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateOptionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSelectableOptionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.OptionDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var Option = _mapper.Map<Hrm.Domain.SelectableOption>(request.OptionDto);

            await _unitOfWork.Repository<Hrm.Domain.SelectableOption>().Add(Option);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = Option.OptionId;

            return response;
        }
    }
}

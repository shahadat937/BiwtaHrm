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
    public class UpdateOptionCommandHandler: IRequestHandler<UpdateOptionCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOptionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateOptionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateSelectableOptionDtoValidator();
            var validationResult = await validator.ValidateAsync(request.OptionDto);

            if(!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var Option = await _unitOfWork.Repository<Hrm.Domain.SelectableOption>().Get(request.OptionDto.OptionId);

            if(Option==null)
            {
                throw new NotFoundException(nameof(Option), request.OptionDto.OptionId);
            }

            _mapper.Map(request.OptionDto, Option);

            await _unitOfWork.Repository<Hrm.Domain.SelectableOption>().Update(Option);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = Option.OptionId;

            return response;
        }
    }
}

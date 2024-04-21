using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Pool.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Pool.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Pool.Handlers.Commands
{
    public class UpdatePoolCommandHandler : IRequestHandler<UpdatePoolCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePoolCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePoolCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdatePoolDtoValidator();
            var validationResult = await validator.ValidateAsync(request.PoolDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Pool = await _unitOfWork.Repository<Hrm.Domain.Pool>().Get(request.PoolDto.PoolId);

            if (Pool is null)
            {
                throw new NotFoundException(nameof(Pool), request.PoolDto.PoolId);
            }

            _mapper.Map(request.PoolDto, Pool);

            await _unitOfWork.Repository<Hrm.Domain.Pool>().Update(Pool);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

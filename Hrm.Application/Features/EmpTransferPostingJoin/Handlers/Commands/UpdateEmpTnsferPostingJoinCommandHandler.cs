using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpTnsferPostingJoin.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.EmpTnsferPostingJoin.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTnsferPostingJoin.Handlers.Commands
{
    public class UpdateEmpTnsferPostingJoinCommandHandler : IRequestHandler<UpdateEmpTnsferPostingJoinCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateEmpTnsferPostingJoinCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEmpTnsferPostingJoinCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateEmpTnsferPostingJoinDtoValidators();
            var validationResult = await validator.ValidateAsync(request.EmpTnsferPostingJoinDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var EmpTnsferPostingJoin = await _unitOfWork.Repository<Hrm.Domain.EmpTnsferPostingJoin>().Get(request.EmpTnsferPostingJoinDto.EmpTnsferPostingJoinId);

            if (EmpTnsferPostingJoin is null)
            {
                throw new NotFoundException(nameof(EmpTnsferPostingJoin), request.EmpTnsferPostingJoinDto.EmpTnsferPostingJoinId);
            }

            _mapper.Map(request.EmpTnsferPostingJoinDto, EmpTnsferPostingJoin);

            await _unitOfWork.Repository<Hrm.Domain.EmpTnsferPostingJoin>().Update(EmpTnsferPostingJoin);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Branch.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Branch.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Branch.Handlers.Commands
{
    public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateBranchDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BranchDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Branch = await _unitOfWork.Repository<Hrm.Domain.Branch>().Get(request.BranchDto.BranchId);

            if (Branch is null)
            {
                throw new NotFoundException(nameof(Branch), request.BranchDto.BranchId);
            }

            _mapper.Map(request.BranchDto, Branch);

            await _unitOfWork.Repository<Hrm.Domain.Branch>().Update(Branch);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

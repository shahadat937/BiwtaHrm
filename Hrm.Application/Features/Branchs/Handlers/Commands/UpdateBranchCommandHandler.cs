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
    public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Branch> _BranchRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Branch> BranchRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _BranchRepository = BranchRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateBranchDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BranchDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var BranchName = request.BranchDto.BranchName.ToLower();

            IQueryable<Hrm.Domain.Branch> Branches = _BranchRepository.Where(x => x.BranchName.ToLower() == BranchName);



            if (Branches.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Branch = await _unitOfWork.Repository<Hrm.Domain.Branch>().Get(request.BranchDto.BranchId);

                if (Branch is null)
                {
                    throw new NotFoundException(nameof(Branch), request.BranchDto.BranchId);
                }

                _mapper.Map(request.BranchDto, Branch);

                await _unitOfWork.Repository<Hrm.Domain.Branch>().Update(Branch);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Branch.BranchId;

            }

            return response;
        }
    }
}

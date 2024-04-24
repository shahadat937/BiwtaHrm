using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SubBranch.Validators;
using Hrm.Application.DTOs.SubBranch.ValidatorsSubBranch;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.SubBranch.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubBranch.Handlers.Commands
{
    public class UpdateSubBranchCommandHandler : IRequestHandler<UpdateSubBranchCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.SubBranch> _SubBranchRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSubBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.SubBranch> SubBranchRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _SubBranchRepository = SubBranchRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateSubBranchCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateSubBranchDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SubBranchDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            //var SubBranchName = request.SubBranchDto.SubBranchName.ToLower();
            var SubBranchName = request.SubBranchDto.SubBranchName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.SubBranch> SubBranches = _SubBranchRepository.Where(x => x.SubBranchName.ToLower() == SubBranchName);



            if (SubBranches.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var SubBranch = await _unitOfWork.Repository<Hrm.Domain.SubBranch>().Get(request.SubBranchDto.SubBranchId);

                if (SubBranch is null)
                {
                    throw new NotFoundException(nameof(SubBranch), request.SubBranchDto.SubBranchId);
                }

                _mapper.Map(request.SubBranchDto, SubBranch);

                await _unitOfWork.Repository<Hrm.Domain.SubBranch>().Update(SubBranch);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = SubBranch.SubBranchId;

            }

            return response;
        }
    }
}

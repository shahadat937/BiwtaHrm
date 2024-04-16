using AutoMapper;
using FluentValidation;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.BloodGroup.Validators;
using Hrm.Application.DTOs.SubBranch.Validators;
using Hrm.Application.DTOs.SubBranchSubBranch.Validators;
using Hrm.Application.Features.SubBranch.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubBranch.Handlers.Commands
{
    public class CreateSubBranchCommandHandler : IRequestHandler<CreateSubBranchCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.SubBranch> _SubBranchRepository; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSubBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.SubBranch> SubBranchRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _SubBranchRepository = SubBranchRepository;
        }


        public async Task<BaseCommandResponse> Handle(CreateSubBranchCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSubBranchDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.SubBranchDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x=> x.ErrorMessage).ToList();
            }
            else
            {

                var SubBranchName = request.SubBranchDto.SubBranchName.ToLower();

                IQueryable<Hrm.Domain.SubBranch> SubBranches = _SubBranchRepository.Where(x => x.SubBranchName.ToLower() == SubBranchName);

                

                if (SubBranches.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed Name already exists.";
                    response.Errors = validatorResult.Errors.Select(q => q.ErrorMessage).ToList();

                }

                else
                {
                    var SubBranch = _mapper.Map<Hrm.Domain.SubBranch>(request.SubBranchDto);

                    SubBranch = await _unitOfWork.Repository<Hrm.Domain.SubBranch>().Add(SubBranch);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = SubBranch.SubBranchId;
                }
            }

            return response;
        }
    }
}

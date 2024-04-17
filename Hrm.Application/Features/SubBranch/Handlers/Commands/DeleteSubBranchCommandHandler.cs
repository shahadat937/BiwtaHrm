using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SubBranch.Validators;
using Hrm.Application.Exceptions;
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
    public class DeleteSubBranchCommandHandler : IRequestHandler<DeleteSubBranchCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSubBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteSubBranchCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var SubBranch = await _unitOfWork.Repository<Hrm.Domain.SubBranch>().Get(request.SubBranchId);

            if (SubBranch == null)
            {
                throw new NotFoundException(nameof(SubBranch), request.SubBranchId);
            }

            await _unitOfWork.Repository<Hrm.Domain.SubBranch>().Delete(SubBranch);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = SubBranch.SubBranchId;

            return response;
        }
    }
}

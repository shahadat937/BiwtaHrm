using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Branch.Validators;
using Hrm.Application.Features.Branch.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Hrm.Application.DTOs.Branch.Validators;
using Hrm.Application.Features.Branch.Requests.Commands;

namespace Hrm.Application.Features.Branch.Handlers.Commands
{
    public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateBranchCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateBranchDtoValidator();
            var validationResult = await validator.ValidateAsync(request.BranchDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Branch = _mapper.Map<Hrm.Domain.Branch>(request.BranchDto);

                Branch = await _unitOfWork.Repository<Hrm.Domain.Branch>().Add(Branch);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = Branch.BranchId;
            }

            return response;
        }

    }
}

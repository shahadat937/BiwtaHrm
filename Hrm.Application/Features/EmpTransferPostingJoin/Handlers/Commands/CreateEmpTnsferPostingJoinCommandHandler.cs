using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpTnsferPostingJoin.Validators;
using Hrm.Application.Features.EmpTnsferPostingJoin.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;

namespace Hrm.Application.Features.EmpTnsferPostingJoin.Handlers.Commands
{
    public class CreateEmpTnsferPostingJoinCommandHandler : IRequestHandler<CreateEmpTnsferPostingJoinCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateEmpTnsferPostingJoinCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateEmpTnsferPostingJoinCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateEmpTnsferPostingJoinDtoValidator();
            var validationResult = await validator.ValidateAsync(request.EmpTnsferPostingJoinDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var EmpTnsferPostingJoin = _mapper.Map<Hrm.Domain.EmpTnsferPostingJoin>(request.EmpTnsferPostingJoinDto);

                EmpTnsferPostingJoin = await _unitOfWork.Repository<Hrm.Domain.EmpTnsferPostingJoin>().Add(EmpTnsferPostingJoin);
                await _unitOfWork.Save();


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = EmpTnsferPostingJoin.EmpTnsferPostingJoinId;
            }

            return response;
        }

    }
}

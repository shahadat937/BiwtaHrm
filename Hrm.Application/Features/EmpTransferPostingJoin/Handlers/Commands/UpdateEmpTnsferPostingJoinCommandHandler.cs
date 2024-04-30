using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpTnsferPostingJoin.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.EmpTnsferPostingJoin.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTnsferPostingJoin.Handlers.Commands
{
    public class UpdateEmpTnsferPostingJoinCommandHandler : IRequestHandler<UpdateEmpTnsferPostingJoinCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.EmpTnsferPostingJoin> _EmpTnsferPostingJoinRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateEmpTnsferPostingJoinCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.EmpTnsferPostingJoin> EmpTnsferPostingJoinRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _EmpTnsferPostingJoinRepository = EmpTnsferPostingJoinRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateEmpTnsferPostingJoinCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateEmpTnsferPostingJoinDtoValidators();
            var validationResult = await validator.ValidateAsync(request.EmpTnsferPostingJoinDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var EmpTnsferPostingJoin = await _unitOfWork.Repository<Hrm.Domain.EmpTnsferPostingJoin>().Get(request.EmpTnsferPostingJoinDto.EmpTnsferPostingJoinId);

            if (EmpTnsferPostingJoin is null)
            {
                throw new NotFoundException(nameof(EmpTnsferPostingJoin), request.EmpTnsferPostingJoinDto.EmpTnsferPostingJoinId);
            }

            //var EmpTnsferPostingJoinName = request.EmpTnsferPostingJoinDto.EmpTnsferPostingJoinName.ToLower();
            var EmpTnsferPostingJoinName = request.EmpTnsferPostingJoinDto.EmpTnsferPostingJoinName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.EmpTnsferPostingJoin> EmpTnsferPostingJoins = _EmpTnsferPostingJoinRepository.Where(x => x.EmpTnsferPostingJoinName.ToLower() == EmpTnsferPostingJoinName);


            if (EmpTnsferPostingJoins.Any())
            {
                response.Success = false;
                // response.Message = "Creation Failed Name already exists.";
                response.Message = $"Creation Failed '{request.EmpTnsferPostingJoinDto.EmpTnsferPostingJoinName}' already exists.";

                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.EmpTnsferPostingJoinDto, EmpTnsferPostingJoin);

                await _unitOfWork.Repository<Hrm.Domain.EmpTnsferPostingJoin>().Update(EmpTnsferPostingJoin);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = EmpTnsferPostingJoin.EmpTnsferPostingJoinId;

            }
            return response;
        }
    }
}

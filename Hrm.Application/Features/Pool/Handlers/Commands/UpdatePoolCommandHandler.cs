using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Pool.Validators;
using Hrm.Application.DTOs.GradeType.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Pool.Requests.Commands;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Pool.Handlers.Commands
{
    public class UpdatePoolCommandHandler : IRequestHandler<UpdatePoolCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Pool> _PoolRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePoolCommandHandler(IHrmRepository<Hrm.Domain.Pool> PoolRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _PoolRepository = PoolRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdatePoolCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdatePoolDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.PoolDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            //var PoolName = request.PoolDto.PoolName.ToLower();
            var PoolName = request.PoolDto.PoolName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Pool> Pooles = _PoolRepository.Where(x => x.PoolName.ToLower() == PoolName);

            if (Pooles.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.PoolDto.PoolName}' already exists.";

                //response.Message = "Creation Failed, Name already exists";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            else
            {

                var Pool = await _unitOfWork.Repository<Hrm.Domain.Pool>().Get(request.PoolDto.PoolId);

                if (Pool is null)
                {
                    throw new NotFoundException(nameof(Pool), request.PoolDto.PoolId);
                }

                _mapper.Map(request.PoolDto, Pool);

                await _unitOfWork.Repository<Hrm.Domain.Pool>().Update(Pool);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Pool.PoolId;

            }

            return response;
        }
    }
}


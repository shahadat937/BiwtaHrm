using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Pool.Validators;
using Hrm.Application.Features.Pool.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.Pool.Handlers.Commands
{
    public class CreatePoolCommandHandler : IRequestHandler<CreatePoolCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Pool> _PoolRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePoolCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Pool> PoolRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _PoolRepository = PoolRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreatePoolCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreatePoolDtoValidator();
            var validationResult = await validator.ValidateAsync(request.PoolDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var PoolName = request.PoolDto.PoolName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.Pool> Pools = _PoolRepository.Where(x => x.PoolName.ToLower().Replace(" ", string.Empty) == PoolName);


                if (PoolNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.PoolDto.PoolName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var Pool = _mapper.Map<Hrm.Domain.Pool>(request.PoolDto);

                    Pool = await _unitOfWork.Repository<Hrm.Domain.Pool>().Add(Pool);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = Pool.PoolId;
                }
            }

            return response;
        }
        private bool PoolNameExists(CreatePoolCommand request)
        {
            var PoolName = request.PoolDto.PoolName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Pool> Pools = _PoolRepository.Where(x => x.PoolName.Trim().ToLower().Replace(" ", string.Empty) == PoolName);

            return Pools.Any();
        }
    }
}

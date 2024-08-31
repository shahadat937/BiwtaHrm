using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.RewardPunishmentTypes.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RewardPunishmentType.Handlers.Commands
{
    public class UpdateRewardPunishmentTypeCommandHandler : IRequestHandler<UpdateRewardPunishmentTypeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.RewardPunishmentType> _RewardPunishmentTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRewardPunishmentTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.RewardPunishmentType> RewardPunishmentTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _RewardPunishmentTypeRepository = RewardPunishmentTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateRewardPunishmentTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            IQueryable<Hrm.Domain.RewardPunishmentType> RewardPunishmentTypes = _RewardPunishmentTypeRepository.Where(x => x.Name == request.RewardPunishmentTypeDto.Name && x.Id != request.RewardPunishmentTypeDto.Id);



            if (RewardPunishmentTypes.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.RewardPunishmentTypeDto.Name}' already exists.";
            }

            else
            {

                var RewardPunishmentType = await _unitOfWork.Repository<Hrm.Domain.RewardPunishmentType>().Get(request.RewardPunishmentTypeDto.Id);

                if (RewardPunishmentType is null)
                {
                    response.Success = false;
                    response.Message = $"Update Failed '{request.RewardPunishmentTypeDto.Id}' not found.";
                }

                _mapper.Map(request.RewardPunishmentTypeDto, RewardPunishmentType);

                await _unitOfWork.Repository<Hrm.Domain.RewardPunishmentType>().Update(RewardPunishmentType);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = RewardPunishmentType.Id;

            }

            return response;
        }
    }
}

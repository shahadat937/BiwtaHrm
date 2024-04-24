using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Leave.Validators;
using Hrm.Application.DTOs.GradeType.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Leave.Requests.Commands;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Leave.Handlers.Commands
{
    public class UpdateLeaveCommandHandler : IRequestHandler<UpdateLeaveCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Leave> _LeaveRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveCommandHandler(IHrmRepository<Hrm.Domain.Leave> LeaveRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _LeaveRepository = LeaveRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateLeaveCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateLeaveDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.LeaveDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            //var LeaveName = request.LeaveDto.LeaveName.ToLower();
            var LeaveName = request.LeaveDto.LeaveName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Leave> Leavees = _LeaveRepository.Where(x => x.LeaveName.ToLower() == LeaveName);

            if (Leavees.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.LeaveDto.LeaveName}' already exists.";

                //response.Message = "Creation Failed, Name already exists";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            else
            {

                var Leave = await _unitOfWork.Repository<Hrm.Domain.Leave>().Get(request.LeaveDto.LeaveId);

                if (Leave is null)
                {
                    throw new NotFoundException(nameof(Leave), request.LeaveDto.LeaveId);
                }

                _mapper.Map(request.LeaveDto, Leave);

                await _unitOfWork.Repository<Hrm.Domain.Leave>().Update(Leave);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Leave.LeaveId;

            }

            return response;
        }
    }
}


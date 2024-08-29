using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Shift.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Shift.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Shift.Handlers.Commands
{
    public class UpdateShiftCommandHandler : IRequestHandler<UpdateShiftCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateShiftCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateShiftCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Shift = await _unitOfWork.Repository<Hrm.Domain.Shift>().Get(request.ShiftDto.ShiftId);

            if (Shift is null)
            {
                response.Success = false;
                response.Message = "Creation Failed";
            }

            _mapper.Map(request.ShiftDto, Shift);

            await _unitOfWork.Repository<Hrm.Domain.Shift>().Update(Shift);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";
            response.Id = Shift.ShiftId;

            return response;
        }
    }
}

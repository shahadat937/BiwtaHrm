using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.NavbarThems.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.NavbarThems.Handlers.Commands
{
    public class UpdateNavbarThemCommandHandler : IRequestHandler<UpdateNavbarThemCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateNavbarThemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateNavbarThemCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var NavbarThem = await _unitOfWork.Repository<Hrm.Domain.NavbarThem>().Get(request.NavbarThemDto.Id);

            if (NavbarThem is null)
            {
                response.Success = false;
                response.Message = "Update Failed";
            }

            _mapper.Map(request.NavbarThemDto, NavbarThem);

            await _unitOfWork.Repository<Hrm.Domain.NavbarThem>().Update(NavbarThem);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";
            response.Id = NavbarThem.Id;

            return response;
        }
    }
}

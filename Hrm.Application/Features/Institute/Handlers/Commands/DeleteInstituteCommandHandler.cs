using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Institute.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Institute.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Institute.Handlers.Commands
{
    public class DeleteInstituteCommandHandler : IRequestHandler<DeleteInstituteCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteInstituteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteInstituteCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Institute = await _unitOfWork.Repository<Hrm.Domain.Institute>().Get(request.InstituteId);

            if (Institute == null)
            {
                throw new NotFoundException(nameof(Institute), request.InstituteId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Institute>().Delete(Institute);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Institute.InstituteId;

            return response;
        }
    }
}

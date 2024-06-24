using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Year.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Year.Handlers.Commands
{
    public class DeleteYearCommandHandler : IRequestHandler<DeleteYearCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteYearCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteYearCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Year = await _unitOfWork.Repository<Hrm.Domain.Year>().Get(request.YearId);

            if (Year == null)
            {
                throw new NotFoundException(nameof(Year), request.YearId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Year>().Delete(Year);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Year.YearId;

            return response;
        }
    }
}

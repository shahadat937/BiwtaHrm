using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Workday.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Workday.Handlers.Commands
{
    public class UpdateWorkdayCommandHandler : IRequestHandler<UpdateWorkdayCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateWorkdayCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateWorkdayCommand request, CancellationToken cancellationToken)
        {
            var workday = await _unitOfWork.Repository<Hrm.Domain.Workday>().Get(request.WorkdayDto.WorkdayId);

            if(workday == null)
            {
                throw new NotFoundException(nameof(workday), request.WorkdayDto.WorkdayId);
            }

            _mapper.Map(request.WorkdayDto, workday);
            await _unitOfWork.Repository<Hrm.Domain.Workday>().Update(workday);
            await _unitOfWork.Save();

            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Update Successfull";


            return response;

        }
    }
}

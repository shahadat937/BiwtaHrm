using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.CourseDurations.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.CourseDuration.Handlers.Commands
{
    public class DeleteCourseDurationCommandHandler : IRequestHandler<DeleteCourseDurationCommand, BaseCommandResponse>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCourseDurationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteCourseDurationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var CourseDuration = await _unitOfWork.Repository<Hrm.Domain.CourseDuration>().Get(request.CourseDurationId);

            if (CourseDuration == null)
            {
                throw new NotFoundException(nameof(CourseDuration), request.CourseDurationId);
            }

            await _unitOfWork.Repository<Hrm.Domain.CourseDuration>().Delete(CourseDuration);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = CourseDuration.Id;

            return response;
        }
    }
}

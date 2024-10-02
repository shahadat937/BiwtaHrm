using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.CourseDurations.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.CourseDuration.Handlers.Commands
{
    public class UpdateCourseDurationCommandHandler : IRequestHandler<UpdateCourseDurationCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.CourseDuration> _CourseDurationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCourseDurationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.CourseDuration> CourseDurationRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _CourseDurationRepository = CourseDurationRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateCourseDurationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            IQueryable<Hrm.Domain.CourseDuration> CourseDurations = _CourseDurationRepository.Where(x => x.Duration == request.CourseDurationDto.Duration && x.Id != request.CourseDurationDto.Id);



            if (CourseDurations.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.CourseDurationDto.Duration}' already exists.";
            }

            else
            {

                var CourseDuration = await _unitOfWork.Repository<Hrm.Domain.CourseDuration>().Get(request.CourseDurationDto.Id);

                if (CourseDuration is null)
                {
                    response.Success = false;
                    response.Message = $"Update Failed '{request.CourseDurationDto.Id}' not found.";
                }

                _mapper.Map(request.CourseDurationDto, CourseDuration);

                await _unitOfWork.Repository<Hrm.Domain.CourseDuration>().Update(CourseDuration);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = CourseDuration.Id;

            }

            return response;
        }
    }
}

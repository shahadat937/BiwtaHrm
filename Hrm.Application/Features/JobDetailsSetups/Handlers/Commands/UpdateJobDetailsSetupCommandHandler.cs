using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.JobDetailsSetups.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.JobDetailsSetup.Handlers.Commands
{
    public class UpdateJobDetailsSetupCommandHandler : IRequestHandler<UpdateJobDetailsSetupCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.JobDetailsSetup> _JobDetailsSetupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateJobDetailsSetupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.JobDetailsSetup> JobDetailsSetupRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _JobDetailsSetupRepository = JobDetailsSetupRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateJobDetailsSetupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var JobDetailsSetup = await _unitOfWork.Repository<Hrm.Domain.JobDetailsSetup>().Get(request.JobDetailsSetupDto.Id);

            if (JobDetailsSetup is null)
            {
                throw new NotFoundException(nameof(JobDetailsSetup), request.JobDetailsSetupDto.Id);
            }

            _mapper.Map(request.JobDetailsSetupDto, JobDetailsSetup);

            await _unitOfWork.Repository<Hrm.Domain.JobDetailsSetup>().Update(JobDetailsSetup);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successful";
            response.Id = JobDetailsSetup.Id;

            return response;
        }
    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.JobDetailsSetups.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.JobDetailsSetup.Handlers.Commands
{
    public class CreateJobDetailsSetupCommandHandler : IRequestHandler<CreateJobDetailsSetupCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.JobDetailsSetup> _JobDetailsSetupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateJobDetailsSetupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.JobDetailsSetup> JobDetailsSetupRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _JobDetailsSetupRepository = JobDetailsSetupRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateJobDetailsSetupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var JobDetailsSetup = _mapper.Map<Hrm.Domain.JobDetailsSetup>(request.JobDetailsSetupDto);

            JobDetailsSetup = await _unitOfWork.Repository<Hrm.Domain.JobDetailsSetup>().Add(JobDetailsSetup);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = JobDetailsSetup.Id;

            return response;
        }
    }
}

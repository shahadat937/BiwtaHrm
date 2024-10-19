using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.JobDetailsSetups.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.JobDetailsSetups.Handlers.Commands
{
    public class UpdateJobDetailsSetupCommandHandler : IRequestHandler<UpdateJobDetailsSetupCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateJobDetailsSetupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateJobDetailsSetupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var JobDetailsSetup = await _unitOfWork.Repository<JobDetailsSetup>().Get(request.JobDetailsSetupDto.Id);


            var findActive = await _unitOfWork.Repository<JobDetailsSetup>().Where(x => x.IsActive == true && x.Id != request.JobDetailsSetupDto.Id).ToListAsync();

            if (findActive != null && request.JobDetailsSetupDto.IsActive == true)
            {
                foreach (var item in findActive)
                {
                    item.IsActive = false;
                    await _unitOfWork.Repository<JobDetailsSetup>().Update(item);
                }
            }

            _mapper.Map(request.JobDetailsSetupDto, JobDetailsSetup);


            await _unitOfWork.Repository<JobDetailsSetup>().Update(JobDetailsSetup);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Update Successful";
            response.Id = JobDetailsSetup.Id;

            return response;
        }
    }
}
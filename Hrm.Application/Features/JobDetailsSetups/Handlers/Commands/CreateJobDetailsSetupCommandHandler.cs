using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.JobDetailsSetups.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.JobDetailsSetups.Handlers.Commands
{
    public class CreateJobDetailsSetupCommandHandler : IRequestHandler<CreateJobDetailsSetupCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateJobDetailsSetupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateJobDetailsSetupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var findActive = await _unitOfWork.Repository<JobDetailsSetup>().Where(x => x.IsActive == true).ToListAsync();

            if (findActive != null && request.JobDetailsSetupDto.IsActive == true)
            {
                foreach (var item in findActive)
                {
                    item.IsActive = false;
                    await _unitOfWork.Repository<JobDetailsSetup>().Update(item);
                }
            }

            var jobDetailsSetup = _mapper.Map<Hrm.Domain.JobDetailsSetup>(request.JobDetailsSetupDto);

            await _unitOfWork.Repository<JobDetailsSetup>().Add(jobDetailsSetup);
            await _unitOfWork.Save();


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = request.JobDetailsSetupDto.Id;

            return response;
        }
    }
}

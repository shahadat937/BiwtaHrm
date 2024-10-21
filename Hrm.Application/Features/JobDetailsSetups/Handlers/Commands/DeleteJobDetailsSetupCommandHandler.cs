using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.JobDetailsSetups.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.JobDetailsSetups.Handlers.Commands
{
    public class DeleteJobDetailsSetupCommandHandler : IRequestHandler<DeleteJobDetailsSetupCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteJobDetailsSetupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteJobDetailsSetupCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var JobDetailsSetup = await _unitOfWork.Repository<Hrm.Domain.JobDetailsSetup>().Get(request.Id);

            if (JobDetailsSetup == null)
                throw new NotFoundException(nameof(JobDetailsSetup), request.Id);

            await _unitOfWork.Repository<Hrm.Domain.JobDetailsSetup>().Delete(JobDetailsSetup);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successful";
            response.Id = JobDetailsSetup.Id;

            return response;
        }
    }
}
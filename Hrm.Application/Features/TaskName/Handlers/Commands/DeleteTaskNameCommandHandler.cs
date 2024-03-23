using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.TaskName.Requests.Commands;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TaskName.Handlers.Commands
{ 

    public class DeleteTaskNameCommandHandler : IRequestHandler<DeleteTaskNameCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteTaskNameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteTaskNameCommand request, CancellationToken cancellationToken)
        {
            var TaskName = await _unitOfWork.Repository<Hrm.Domain.TaskName>().Get(request.TaskNameId);

            if (TaskName == null)
                throw new NotFoundException(nameof(TaskName), request.TaskNameId);

            await _unitOfWork.Repository<Hrm.Domain.TaskName>().Delete(TaskName);
            try
            {
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            //await _unitOfWork.Save();

            return Unit.Value;
        }
    }

}

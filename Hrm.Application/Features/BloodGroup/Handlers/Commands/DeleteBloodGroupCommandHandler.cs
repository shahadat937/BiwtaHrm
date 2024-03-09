using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.BloodGroup.Requests.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.BloodGroup.Handlers.Commands
{
    public class DeleteBloodGroupCommandHandler : IRequestHandler<DeleteBloodGroupCommand>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBloodGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBloodGroupCommand request, CancellationToken cancellationToken)
        {
            var BloodGroup = await _unitOfWork.Repository<Hrm.Domain.BloodGroup>().Get(request.BloodGroupId);

            if (BloodGroup == null)
            {
                throw new NotFoundException(nameof(BloodGroup), request.BloodGroupId);
            }

            await _unitOfWork.Repository<Hrm.Domain.BloodGroup>().Delete(BloodGroup);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.GradeType.Handlers.Commands
{
    public class DeleteGradeTypeCommandHandler : IRequestHandler<DeleteGradeTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteGradeTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteGradeTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var GradeTypes = await _unitOfWork.Repository<Hrm.Domain.GradeType>().Get(request.GradeTypeId);

            if (GradeTypes == null)
            {
                throw new NotFoundException(nameof(GradeTypes), request.GradeTypeId);
            }

            await _unitOfWork.Repository<Hrm.Domain.GradeType>().Delete(GradeTypes);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = GradeTypes.GradeTypeId;

            return response;
        }
    }
}

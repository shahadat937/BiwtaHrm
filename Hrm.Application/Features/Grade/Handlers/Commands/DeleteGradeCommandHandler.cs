using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Grade.Requests.Commands;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Grade.Handlers.Commands
{
    public class DeleteGradeCommandHandler : IRequestHandler<DeleteGradeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteGradeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteGradeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var Grades = await _unitOfWork.Repository<Hrm.Domain.Grade>().Get(request.GradeId);

            if (Grades == null)
            {
                throw new NotFoundException(nameof(Grades), request.GradeId);
            }

            await _unitOfWork.Repository<Hrm.Domain.Grade>().Delete(Grades);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = Grades.GradeId;

            return response;
        }
    }
}
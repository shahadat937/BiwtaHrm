using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.GradeClass.Requests.Commands;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.GradeClass.Handlers.Commands
{
    public class DeleteGradeClassCommandHandler : IRequestHandler<DeleteGradeClassCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteGradeClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(DeleteGradeClassCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();


            var GradeClasses = await _unitOfWork.Repository<Hrm.Domain.GradeClass>().Get(request.GradeClassId);

            if (GradeClasses == null)
            {
                throw new NotFoundException(nameof(GradeClasses), request.GradeClassId);
            }

            await _unitOfWork.Repository<Hrm.Domain.GradeClass>().Delete(GradeClasses);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Delete Successfull";
            response.Id = GradeClasses.GradeClassId;

            return response;
        }
    }
}
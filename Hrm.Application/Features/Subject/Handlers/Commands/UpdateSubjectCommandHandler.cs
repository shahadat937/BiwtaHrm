using AutoMapper;
using FluentValidation.Results;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Subject.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Subject.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Subject.Handlers.Commands
{
    public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, Unit>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSubjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            var respose = new BaseCommandResponse();
            var validator = new UpdateSubjectDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SubjectDto);

            if (validationResult.IsValid == false)
            {
                respose.Success = false;
                respose.Message = "Creation Failed";
                respose.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Subject = await _unitOfWork.Repository<Hrm.Domain.Subject>().Get(request.SubjectDto.SubjectId);

            if (Subject is null)
            {
                throw new NotFoundException(nameof(Subject), request.SubjectDto.SubjectId);
            }

            _mapper.Map(request.SubjectDto, Subject);

            await _unitOfWork.Repository<Hrm.Domain.Subject>().Update(Subject);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

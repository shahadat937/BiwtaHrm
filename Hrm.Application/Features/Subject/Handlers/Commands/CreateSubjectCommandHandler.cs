﻿using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Subject.Validators;
using Hrm.Application.Features.Subject.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.Subject.Handlers.Commands
{
    public class CreateSubjectCommandHandler : IRequestHandler<CreateSubjectCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Subject> _SubjectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateSubjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Subject> SubjectRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _SubjectRepository = SubjectRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSubjectDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SubjectDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var SubjectName = request.SubjectDto.SubjectName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.Subject> Subjects = _SubjectRepository.Where(x => x.SubjectName.ToLower().Replace(" ", string.Empty) == SubjectName);


                if (SubjectNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.SubjectDto.SubjectName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var Subject = _mapper.Map<Hrm.Domain.Subject>(request.SubjectDto);

                    Subject = await _unitOfWork.Repository<Hrm.Domain.Subject>().Add(Subject);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = Subject.SubjectId;
                }
            }

            return response;
        }
        private bool SubjectNameExists(CreateSubjectCommand request)
        {
            var SubjectName = request.SubjectDto.SubjectName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Subject> Subjects = _SubjectRepository.Where(x => x.SubjectName.Trim().ToLower().Replace(" ", string.Empty) == SubjectName);

            return Subjects.Any();
        }
    }
}

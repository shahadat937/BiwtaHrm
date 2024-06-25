using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Subject.Validators;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Subject.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Subject.Handlers.Commands
{
    public class UpdateSubjectCommandHandler : IRequestHandler<UpdateSubjectCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Subject> _SubjectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSubjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Subject> SubjectRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _SubjectRepository = SubjectRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateSubjectDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SubjectDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            
            var SubjectName = request.SubjectDto.SubjectName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.Subject> Subjects = _SubjectRepository.Where(x => x.SubjectName.ToLower() == SubjectName);



            if (Subjects.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.SubjectDto.SubjectName}' already exists.";

                //response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Subject = await _unitOfWork.Repository<Hrm.Domain.Subject>().Get(request.SubjectDto.SubjectId);

                if (Subject is null)
                {
                    throw new NotFoundException(nameof(Subject), request.SubjectDto.SubjectId);
                }

                _mapper.Map(request.SubjectDto, Subject);

                await _unitOfWork.Repository<Hrm.Domain.Subject>().Update(Subject);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Subject.SubjectId;

            }

            return response;
        }
    }
}

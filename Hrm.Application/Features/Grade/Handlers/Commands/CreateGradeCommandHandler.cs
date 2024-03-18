using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Grade.Validators;
using Hrm.Application.DTOs.GradeType.Validators;
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
    public class CreateGradeCommandHandler : IRequestHandler<CreateGradeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Grade> _gradeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateGradeCommandHandler(IHrmRepository<Hrm.Domain.Grade> gradeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateGradeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateGradeDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.GradeDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var gradeName = request.GradeDto.GradeName.ToLower();

                IQueryable<Hrm.Domain.Grade> grades = _gradeRepository.Where(x => x.GradeName.ToLower() == gradeName);

                if (grades.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed, Name already exists";
                    response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                }
                else
                {
                    var Grade = _mapper.Map<Hrm.Domain.Grade>(request.GradeDto);

                    Grade = await _unitOfWork.Repository<Hrm.Domain.Grade>().Add(Grade);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = Grade.GradeId;
                }
            }
            return response;
        }
    }
}
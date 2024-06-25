using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Grade.Validators;
using Hrm.Application.DTOs.GradeType.Validators;
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
    public class UpdateGradeCommandHandler : IRequestHandler<UpdateGradeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Grade> _gradeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateGradeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Grade> gradeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _gradeRepository = gradeRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateGradeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.GradeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var gradeName = request.GradeDto.GradeName.ToLower();

            IQueryable<Hrm.Domain.Grade> grades = _gradeRepository.Where(x => x.GradeName.ToLower() == gradeName);



            if (grades.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.GradeDto.GradeName}' already exists.";

                // response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Grade = await _unitOfWork.Repository<Hrm.Domain.Grade>().Get(request.GradeDto.GradeId);

                if (Grade is null)
                {
                    throw new NotFoundException(nameof(Grade), request.GradeDto.GradeId);
                }

                _mapper.Map(request.GradeDto, Grade);

                await _unitOfWork.Repository<Hrm.Domain.Grade>().Update(Grade);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Grade.GradeId;

            }

            return response;
        }
        private bool GradeNameExists(CreateGradeCommand request)
        {
            var GradeName = request.GradeDto.GradeName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Grade> Grades = _gradeRepository.Where(x => x.GradeName.Trim().ToLower().Replace(" ", string.Empty) == GradeName);

            return Grades.Any();
        }
    }
}

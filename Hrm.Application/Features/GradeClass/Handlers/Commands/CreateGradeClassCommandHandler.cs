using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.GradeClass.Validators;
using Hrm.Application.DTOs.GradeType.Validators;
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
    public class CreateGradeClassCommandHandler : IRequestHandler<CreateGradeClassCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.GradeClass> _gradeClassRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateGradeClassCommandHandler(IHrmRepository<Hrm.Domain.GradeClass> gradeClassRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _gradeClassRepository = gradeClassRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateGradeClassCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateGradeClassDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.GradeClassDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var gradeClassName = request.GradeClassDto.GradeClassName.ToLower();

                IQueryable<Hrm.Domain.GradeClass> gradeClasses = _gradeClassRepository.Where(x => x.GradeClassName.ToLower() == gradeClassName);

                if (gradeClasses.Any())
                {
                    response.Success = false;
                    response.Message = "Creation Failed, Name already exists";
                    response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                }
                else
                {
                    var GradeClass = _mapper.Map<Hrm.Domain.GradeClass>(request.GradeClassDto);

                    GradeClass = await _unitOfWork.Repository<Hrm.Domain.GradeClass>().Add(GradeClass);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = GradeClass.GradeClassId;
                }
            }
            return response;
        }
    }
}
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.GradeType.Validators;
using Hrm.Application.DTOs.MaritalStatus.Validators;
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
    public class UpdateGradeTypeCommandHandler : IRequestHandler<UpdateGradeTypeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.GradeType> _gradeTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateGradeTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.GradeType> gradeTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _gradeTypeRepository = gradeTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateGradeTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateGradeTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.GradeTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var gradeTypeName = request.GradeTypeDto.GradeTypeName.ToLower();

            IQueryable<Hrm.Domain.GradeType> gradeTypes = _gradeTypeRepository.Where(x => x.GradeTypeName.ToLower() == gradeTypeName);



            if (gradeTypes.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.GradeTypeDto.GradeTypeName}' already exists.";

                //response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var GradeType = await _unitOfWork.Repository<Hrm.Domain.GradeType>().Get(request.GradeTypeDto.GradeTypeId);

                if (GradeType is null)
                {
                    throw new NotFoundException(nameof(GradeType), request.GradeTypeDto.GradeTypeId);
                }

                _mapper.Map(request.GradeTypeDto, GradeType);

                await _unitOfWork.Repository<Hrm.Domain.GradeType>().Update(GradeType);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = GradeType.GradeTypeId;

            }

            return response;
        }
    }
}

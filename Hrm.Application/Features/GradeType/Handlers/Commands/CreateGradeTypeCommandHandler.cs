using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.GradeType.Validators;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Hrm.Application.Features.GradeType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.GradeType.Handlers.Commands
{
    public class CreateGradeTypeCommandHandler : IRequestHandler<CreateGradeTypeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.GradeType> _gradeTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateGradeTypeCommandHandler(IHrmRepository<Hrm.Domain.GradeType> gradeTypeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _gradeTypeRepository = gradeTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateGradeTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateGradeTypeDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.GradeTypeDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var gradeTypeName = request.GradeTypeDto.GradeTypeName.ToLower();

                IQueryable<Hrm.Domain.GradeType> gradeTypes = _gradeTypeRepository.Where(x=>x.GradeTypeName.ToLower() == gradeTypeName);

                if (GradeTypeNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.GradeTypeDto.GradeTypeName}' already exists.";

                    //response.Message = "Creation Failed, Name already exists";
                    response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
                }
                else
                {
                    var GradeType = _mapper.Map<Hrm.Domain.GradeType>(request.GradeTypeDto);

                    GradeType = await _unitOfWork.Repository<Hrm.Domain.GradeType>().Add(GradeType);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = GradeType.GradeTypeId;
                }
            }
            return response;
        }
        private bool GradeTypeNameExists(CreateGradeTypeCommand request)
        {
            var GradeTypeName = request.GradeTypeDto.GradeTypeName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.GradeType> GradeTypes = _gradeTypeRepository.Where(x => x.GradeTypeName.Trim().ToLower().Replace(" ", string.Empty) == GradeTypeName);

            return GradeTypes.Any();
        }
    }
}

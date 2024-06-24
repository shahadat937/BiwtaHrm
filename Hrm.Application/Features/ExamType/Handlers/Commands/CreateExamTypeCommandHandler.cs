using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ExamType.Validators;
using Hrm.Application.Features.ExamType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.ExamType.Handlers.Commands
{
    public class CreateExamTypeCommandHandler : IRequestHandler<CreateExamTypeCommand, BaseCommandResponse>
    {


        private readonly IHrmRepository<Hrm.Domain.ExamType> _ExamTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateExamTypeCommandHandler(IHrmRepository<Hrm.Domain.ExamType> ExamTypeRepository, IUnitOfWork unitOfWork, IMapper mapper)
       
        {
            _ExamTypeRepository = ExamTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ExamTypeRepository = ExamTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateExamTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateExamTypeDtoValidator ();
            var validatorResult = await validator.ValidateAsync(request.ExamTypeDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var ExamTypeName = request.ExamTypeDto.ExamTypeName.ToLower();

                IQueryable<Hrm.Domain.ExamType> ExamTypes = _ExamTypeRepository.Where(x => x.ExamTypeName.ToLower() == ExamTypeName);


                if (ExamTypeNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.ExamTypeDto.ExamTypeName}' already exists.";


                    //response.Message = "Creation Failed, Name already exists";
                    response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();

                }
                else
                {
                    var ExamType = _mapper.Map<Hrm.Domain.ExamType>(request.ExamTypeDto);

                    ExamType = await _unitOfWork.Repository<Hrm.Domain.ExamType>().Add(ExamType);
                    await _unitOfWork.Save();
                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = ExamType.ExamTypeId;
                }
            }

            return response;
        }
        private bool ExamTypeNameExists(CreateExamTypeCommand request)
        {

            var ExamTypeName = request.ExamTypeDto.ExamTypeName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.ExamType> ExamTypes = _ExamTypeRepository.Where(x => x.ExamTypeName.Trim().ToLower().Replace(" ", string.Empty) == ExamTypeName);

            return ExamTypes.Any();

        }
    }
}

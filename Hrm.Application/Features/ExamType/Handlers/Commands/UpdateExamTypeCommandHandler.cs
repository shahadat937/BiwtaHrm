using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.ExamType.Validators;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.ExamType.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ExamType.Handlers.Commands
{
    public class UpdateExamTypeCommandHandler : IRequestHandler<UpdateExamTypeCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.ExamType> _ExamTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public UpdateExamTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.ExamType> ExamTypeRepository)

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ExamTypeRepository = ExamTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateExamTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateExamTypeDtoValidators();
            var validationResult = await validator.ValidateAsync(request.ExamTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            //var ExamTypeName = request.ExamTypeDto.ExamTypeName.ToLower();
            var ExamTypeName = request.ExamTypeDto.ExamTypeName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.ExamType> ExamTypes = _ExamTypeRepository.Where(x => x.ExamTypeName.ToLower() == ExamTypeName);
            if (ExamTypes.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.ExamTypeDto.ExamTypeName}' already exists.";

                //response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var ExamType = await _unitOfWork.Repository<Hrm.Domain.ExamType>().Get(request.ExamTypeDto.ExamTypeId);

                if (ExamType is null)
                {
                    throw new NotFoundException(nameof(ExamType), request.ExamTypeDto.ExamTypeId);
                }

                _mapper.Map(request.ExamTypeDto, ExamType);

                await _unitOfWork.Repository<Hrm.Domain.ExamType>().Update(ExamType);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = ExamType.ExamTypeId;

            }

            return response;
        }
    }
}

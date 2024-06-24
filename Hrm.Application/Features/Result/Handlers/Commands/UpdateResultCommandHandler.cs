using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Result.Validators;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Result.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Result.Handlers.Commands
{
    public class UpdateResultCommandHandler : IRequestHandler<UpdateResultCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Result> _ResultRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateResultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Result> ResultRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ResultRepository = ResultRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateResultCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateResultDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ResultDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            //var ResultName = request.ResultDto.ResultName.ToLower();
            var ResultName = request.ResultDto.ResultName.Trim().ToLower().Replace(" ", string.Empty);
            IQueryable<Hrm.Domain.Result> Results = _ResultRepository.Where(x => x.ResultName.ToLower() == ResultName);



            if (Results.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.ResultDto.ResultName}' already exists.";

                //response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Result = await _unitOfWork.Repository<Hrm.Domain.Result>().Get(request.ResultDto.ResultId);

                if (Result is null)
                {
                    throw new NotFoundException(nameof(Result), request.ResultDto.ResultId);
                }

                _mapper.Map(request.ResultDto, Result);

                await _unitOfWork.Repository<Hrm.Domain.Result>().Update(Result);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Result.ResultId;

            }

            return response;
        }
    }
}

using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Result.Validators;
using Hrm.Application.Features.Result.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.Result.Handlers.Commands
{
    public class CreateResultCommandHandler : IRequestHandler<CreateResultCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Result> _ResultRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateResultCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Result> ResultRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ResultRepository = ResultRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateResultCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateResultDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ResultDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var ResultName = request.ResultDto.ResultName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.Result> Results = _ResultRepository.Where(x => x.ResultName.ToLower().Replace(" ", string.Empty) == ResultName);


                if (ResultNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.ResultDto.ResultName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var Result = _mapper.Map<Hrm.Domain.Result>(request.ResultDto);

                    Result = await _unitOfWork.Repository<Hrm.Domain.Result>().Add(Result);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = Result.ResultId;
                }
            }

            return response;
        }
        private bool ResultNameExists(CreateResultCommand request)
        {
            var ResultName = request.ResultDto.ResultName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Result> Results = _ResultRepository.Where(x => x.ResultName.Trim().ToLower().Replace(" ", string.Empty) == ResultName);

            return Results.Any();
        }
    }
}

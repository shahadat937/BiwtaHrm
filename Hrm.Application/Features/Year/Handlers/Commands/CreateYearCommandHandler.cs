using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Year.Validators;
using Hrm.Application.Features.Year.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.Year.Handlers.Commands
{
    public class CreateYearCommandHandler : IRequestHandler<CreateYearCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Year> _YearRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateYearCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Year> YearRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _YearRepository = YearRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateYearCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            //var validator = new IYearDtoValidator();
            //var validationResult = await validator.ValidateAsync(request.YearDto);

            if (request.YearDto.YearName == null)
            {
                response.Success = false;
                response.Message = "Creation Failed! Year Name is Requires";
                //response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
             //   var YearName = request.YearDto.YearName.Trim().ToLower().Replace(" ", string.Empty);

              //  IQueryable<Hrm.Domain.Year> Years = _YearRepository.Where(x => x.YearName.ToLower().Replace(" ", string.Empty) == YearName);


                if (YearNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.YearDto.YearName}' already exists.";
                    //response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                    
                }
                else
                {
                    var Year = _mapper.Map<Hrm.Domain.Year>(request.YearDto);

                    Year = await _unitOfWork.Repository<Hrm.Domain.Year>().Add(Year);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = Year.YearId;
                }
            }

            return response;
        }
        private bool YearNameExists(CreateYearCommand request)
        {

            IQueryable<Domain.Year> Years = _YearRepository.Where(x => x.YearName == request.YearDto.YearName);

             return Years.Any();
        }
    }
}

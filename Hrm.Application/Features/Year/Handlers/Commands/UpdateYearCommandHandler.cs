using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Year.Validators;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Year.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Year.Handlers.Commands
{
    public class UpdateYearCommandHandler : IRequestHandler<UpdateYearCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Year> _YearRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateYearCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Year> YearRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _YearRepository = YearRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateYearCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateYearDtoValidator();
            var validationResult = await validator.ValidateAsync(request.YearDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            IQueryable<Hrm.Domain.Year> Years = _YearRepository.Where(x => x.YearName == request.YearDto.YearName && x.YearId != request.YearDto.YearId);



            if (Years.Any())
            {
                response.Success = false;
                response.Message = $"Update Failed '{request.YearDto.YearName}' already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }

            else
            {

                var Year = await _unitOfWork.Repository<Hrm.Domain.Year>().Get(request.YearDto.YearId);

                if (Year is null)
                {
                    response.Success = false;
                    response.Message = $"Update Failed '{request.YearDto.YearId}' not found.";
                }

                _mapper.Map(request.YearDto, Year);

                await _unitOfWork.Repository<Hrm.Domain.Year>().Update(Year);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successfull";
                response.Id = Year.YearId;

            }

            return response;
        }
    }
}

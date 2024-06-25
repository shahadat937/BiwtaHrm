using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Division.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Division.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Division.Handlers.Commands
{
    public class UpdateDivisionCommandHandler : IRequestHandler<UpdateDivisionCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.Division> _DivisionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateDivisionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Division> DivisionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _DivisionRepository = DivisionRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateDivisionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateDivisionDtoValidators();
            var validationResult = await validator.ValidateAsync(request.DivisionDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var Division = await _unitOfWork.Repository<Hrm.Domain.Division>().Get(request.DivisionDto.DivisionId);

            if (Division is null)
            {
                throw new NotFoundException(nameof(Division), request.DivisionDto.DivisionId);
            }

            //var DivisionName = request.DivisionDto.DivisionName.ToLower();
            var DivisionName = request.DivisionDto.DivisionName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Division> Divisions = _DivisionRepository.Where(x => x.DivisionName.ToLower() == DivisionName);


            if (Divisions.Any())
            {
                response.Success = false;
                // response.Message = "Creation Failed Name already exists.";
                response.Message = $"Creation Failed '{request.DivisionDto.DivisionName}' already exists.";

                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.DivisionDto, Division);

                await _unitOfWork.Repository<Hrm.Domain.Division>().Update(Division);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = Division.DivisionId;

            }
            return response;
        }
    }
}

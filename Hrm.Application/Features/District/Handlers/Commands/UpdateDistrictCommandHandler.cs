using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.District.Validators;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.District.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.District.Handlers.Commands
{
    public class UpdateDistrictCommandHandler : IRequestHandler<UpdateDistrictCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<Hrm.Domain.District> _DistrictRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateDistrictCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.District> DistrictRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _DistrictRepository = DistrictRepository;
        }

        public async Task<BaseCommandResponse> Handle(UpdateDistrictCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new UpdateDistrictDtoValidators();
            var validationResult = await validator.ValidateAsync(request.DistrictDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            var District = await _unitOfWork.Repository<Hrm.Domain.District>().Get(request.DistrictDto.DistrictId);

            if (District is null)
            {
                throw new NotFoundException(nameof(District), request.DistrictDto.DistrictId);
            }

            var DistrictName = request.DistrictDto.DistrictName.ToLower();

            IQueryable<Hrm.Domain.District> Districts = _DistrictRepository.Where(x => x.DistrictName.ToLower() == DistrictName);


            if (Districts.Any())
            {
                response.Success = false;
                response.Message = "Creation Failed Name already exists.";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

            }
            else
            {

                _mapper.Map(request.DistrictDto, District);

                await _unitOfWork.Repository<Hrm.Domain.District>().Update(District);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Update Successful";
                response.Id = District.DistrictId;

            }
            return response;
        }
    }
}

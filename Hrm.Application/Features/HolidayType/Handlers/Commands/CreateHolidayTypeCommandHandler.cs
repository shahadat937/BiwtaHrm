using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.HolidayType.Validators;
using Hrm.Application.Features.HolidayType.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.HolidayType.Handlers.Commands
{
    public class CreateHolidayTypeCommandHandler : IRequestHandler<CreateHolidayTypeCommand, BaseCommandResponse>
    {


        private readonly IHrmRepository<Hrm.Domain.HolidayType> _HolidayTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateHolidayTypeCommandHandler(IHrmRepository<Hrm.Domain.HolidayType> HolidayTypeRepository, IUnitOfWork unitOfWork, IMapper mapper)

        {
            _HolidayTypeRepository = HolidayTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _HolidayTypeRepository = HolidayTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateHolidayTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateHolidayTypeDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.HolidayTypeDto);

            if (validatorResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var HolidayTypeName = request.HolidayTypeDto.HolidayTypeName.ToLower();

                IQueryable<Hrm.Domain.HolidayType> HolidayTypes = _HolidayTypeRepository.Where(x => x.HolidayTypeName.ToLower() == HolidayTypeName);


                if (HolidayTypeNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.HolidayTypeDto.HolidayTypeName}' already exists.";


                    //response.Message = "Creation Failed, Name already exists";
                    response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();

                }
                else
                {
                    var HolidayType = _mapper.Map<Hrm.Domain.HolidayType>(request.HolidayTypeDto);

                    HolidayType = await _unitOfWork.Repository<Hrm.Domain.HolidayType>().Add(HolidayType);
                    await _unitOfWork.Save();
                    response.Success = true;
                    response.Message = "Creation Successfull";
                    response.Id = HolidayType.HolidayTypeId;
                }
            }

            return response;
        }
        private bool HolidayTypeNameExists(CreateHolidayTypeCommand request)
        {

            var HolidayTypeName = request.HolidayTypeDto.HolidayTypeName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.HolidayType> HolidayTypes = _HolidayTypeRepository.Where(x => x.HolidayTypeName.Trim().ToLower().Replace(" ", string.Empty) == HolidayTypeName);

            return HolidayTypes.Any();

        }
    }
}

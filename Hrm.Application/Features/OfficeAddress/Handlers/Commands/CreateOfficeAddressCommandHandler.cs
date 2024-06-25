using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.OfficeAddress.Validators;
using Hrm.Application.Features.OfficeAddress.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.OfficeAddress.Handlers.Commands
{
    public class CreateOfficeAddressCommandHandler : IRequestHandler<CreateOfficeAddressCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.OfficeAddress> _officeAddressRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateOfficeAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.OfficeAddress> officeAddressRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _officeAddressRepository = officeAddressRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateOfficeAddressCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateOfficeAddressDtoValidator();
            var validationResult = await validator.ValidateAsync(request.OfficeAddressDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var officeAddressName = request.OfficeAddressDto.OfficeAddressName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.OfficeAddress> officeAddresss = _officeAddressRepository.Where(x => x.OfficeAddressName.ToLower().Replace(" ", string.Empty) == officeAddressName);


                if (OfficeAddressNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.OfficeAddressDto.OfficeAddressName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var OfficeAddress = _mapper.Map<Hrm.Domain.OfficeAddress>(request.OfficeAddressDto);

                    OfficeAddress = await _unitOfWork.Repository<Hrm.Domain.OfficeAddress>().Add(OfficeAddress);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = OfficeAddress.OfficeAddressId;
                }
            }

            return response;
        }
        private bool OfficeAddressNameExists(CreateOfficeAddressCommand request)
        {
            var officeAddressName = request.OfficeAddressDto.OfficeAddressName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.OfficeAddress> officeAddresss = _officeAddressRepository.Where(x => x.OfficeAddressName.Trim().ToLower().Replace(" ", string.Empty) == officeAddressName);

            return officeAddresss.Any();
        }
    }
}

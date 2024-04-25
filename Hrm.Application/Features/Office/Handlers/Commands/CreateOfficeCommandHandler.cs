using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Office.Validators;
using Hrm.Application.Features.Office.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;
using Hrm.Application.DTOs.OfficeOffice.Validators;

namespace Hrm.Application.Features.Office.Handlers.Commands
{
    public class CreateOfficeCommandHandler : IRequestHandler<CreateOfficeCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Office> _officeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateOfficeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Office> officeRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _officeRepository = officeRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateOfficeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.OfficeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                //   var officeName = request.OfficeDto.OfficeName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.Office> offices = _officeRepository.Where(x => x.OfficeName.ToLower().Replace(" ", string.Empty) == officeName);


                if (OfficeNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.OfficeDto.OfficeName}' already exists.";
                    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                }
                else
                {
                    var Office = _mapper.Map<Hrm.Domain.Office>(request.OfficeDto);

                    Office = await _unitOfWork.Repository<Hrm.Domain.Office>().Add(Office);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = Office.OfficeId;
                }
            }

            return response;
        }
        private bool OfficeNameExists(CreateOfficeCommand request)
        {
            var officeName = request.OfficeDto.OfficeName.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable<Hrm.Domain.Office> offices = _officeRepository.Where(x => x.OfficeName.Trim().ToLower().Replace(" ", string.Empty) == officeName);

            return offices.Any();
        }
    }
}

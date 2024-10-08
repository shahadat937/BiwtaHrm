using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Designation.Validators;
using Hrm.Application.Features.Designation.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Hrm.Application.DTOs.Designation.Validators;
using Hrm.Application.Features.Designation.Requests.Commands;
using Hrm.Application.DTOs.Designation.Validators;

namespace Hrm.Application.Features.Designation.Handlers.Commands
{
    public class CreateDesignationCommandHandler : IRequestHandler<CreateDesignationCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateDesignationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.Designation> DesignationRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _DesignationRepository = DesignationRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateDesignationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
                //   var DesignationName = request.DesignationDto.DesignationName.Trim().ToLower().Replace(" ", string.Empty);

                //  IQueryable<Hrm.Domain.Designation> Designations = _DesignationRepository.Where(x => x.DesignationName.ToLower().Replace(" ", string.Empty) == DesignationName);


                //if (DesignationNameExists(request))
                //{
                //    response.Success = false;
                //    response.Message = $"Creation Failed '{request.DesignationDto.DesignationName}' already exists.";
                //    response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();

                //}
                //else
                //{
                    var Designation = _mapper.Map<Hrm.Domain.Designation>(request.DesignationDto);

                    Designation = await _unitOfWork.Repository<Hrm.Domain.Designation>().Add(Designation);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = Designation.DesignationId;
                //}

            return response;
        }
    }
}

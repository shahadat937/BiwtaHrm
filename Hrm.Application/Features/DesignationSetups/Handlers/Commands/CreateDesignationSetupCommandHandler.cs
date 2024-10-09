using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.DesignationSetups.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using Hrm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.DesignationSetup.Handlers.Commands
{
    public class CreateDesignationSetupCommandHandler : IRequestHandler<CreateBloodCommand, BaseCommandResponse>
    {
        private readonly IHrmRepository<Hrm.Domain.DesignationSetup> _DesignationSetupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateDesignationSetupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<Hrm.Domain.DesignationSetup> DesignationSetupRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _DesignationSetupRepository = DesignationSetupRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateBloodCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
             //   var DesignationSetupName = request.DesignationSetupDto.DesignationSetupName.Trim().ToLower().Replace(" ", string.Empty);

              //  IQueryable<Hrm.Domain.DesignationSetup> DesignationSetups = _DesignationSetupRepository.Where(x => x.DesignationSetupName.ToLower().Replace(" ", string.Empty) == DesignationSetupName);


                if (DesignationSetupNameExists(request))
                {
                    response.Success = false;
                    response.Message = $"Creation Failed '{request.DesignationSetupDto.Name}' already exists.";
                    
                }
                else
                {
                    var DesignationSetup = _mapper.Map<Hrm.Domain.DesignationSetup>(request.DesignationSetupDto);

                    DesignationSetup = await _unitOfWork.Repository<Hrm.Domain.DesignationSetup>().Add(DesignationSetup);
                    await _unitOfWork.Save();


                    response.Success = true;
                    response.Message = "Creation Successful";
                    response.Id = DesignationSetup.Id;
                }
            

            return response;
        }
        private bool DesignationSetupNameExists(CreateBloodCommand request)
        {
            var DesignationSetupName = request.DesignationSetupDto.Name.Trim().ToLower().Replace(" ", string.Empty);

            IQueryable <Hrm.Domain.DesignationSetup > DesignationSetups = _DesignationSetupRepository.Where(x => x.Name.Trim().ToLower().Replace(" ", string.Empty) == DesignationSetupName);

             return DesignationSetups.Any();
        }
    }
}

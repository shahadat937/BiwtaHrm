using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SiteVisit.Validators;
using Hrm.Application.Features.appraisalFormType.Requests.Commands;
using Hrm.Application.Features.EmpBasicInfos.Handlers.Queries;
using Hrm.Application.Features.SiteVisit.Requests.Commands;
using Hrm.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SiteVisit.Handlers.Commands
{
    public class UpdateSiteVisitCommandHandler: IRequestHandler<UpdateSiteVisitCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateSiteVisitCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateSiteVisitCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new BaseCommandResponse();
            var validator = new UpdateSiteVisitDtoValidator();

            var validationResult = await validator.ValidateAsync(request.SiteVisitDto);

            if (validationResult.isValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return response;
            }

            var SiteVisit = await _unitOfWork.Repository<Hrm.Domain.SiteVisit>().Get(request.SiteVisitDto.SiteVisitId);

            if(SiteVisit == null)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                return response;
            }

            _mapper.Map(request.SiteVisitDto, SiteVisit);

            await _unitOfWork.Repository<Hrm.Domain.SiteVisit>().Update(SiteVisit);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Update Successfull";
            response.Id = SiteVisit.SiteVisitId;

            return response;
        }
    }
}

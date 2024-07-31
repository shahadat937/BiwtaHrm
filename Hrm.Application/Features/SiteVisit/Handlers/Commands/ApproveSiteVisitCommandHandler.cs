using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.SiteVisit.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Helpers;

namespace Hrm.Application.Features.SiteVisit.Handlers.Commands
{
    public class ApproveSiteVisitCommandHandler: IRequestHandler<ApproveSiteVisitCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SiteVisitAtdHelper _siteVisitAtdHelper;

        public ApproveSiteVisitCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _siteVisitAtdHelper = new SiteVisitAtdHelper(unitOfWork, mapper);
        }

        public async Task<BaseCommandResponse> Handle(ApproveSiteVisitCommand request, CancellationToken cancellationToken)
        {
            if (request==null)
            {
                throw new BadRequestException("Invalid Request");
            }

            var sitevisit = await _unitOfWork.Repository<Hrm.Domain.SiteVisit>().Get(request.SiteVisitId);

            if(sitevisit==null)
            {
                throw new NotFoundException(nameof(sitevisit), request.SiteVisitId);
            }

            sitevisit.Status = "Approved";

            _siteVisitAtdHelper.siteVisitId = request.SiteVisitId;

            await _siteVisitAtdHelper.saveAttendance((DateOnly)sitevisit.FromDate,(DateOnly)sitevisit.ToDate,sitevisit.EmpId);

            await _unitOfWork.Repository<Hrm.Domain.SiteVisit>().Update(sitevisit);
            await _unitOfWork.Save();

            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Approved";
            response.Id = sitevisit.SiteVisitId;

            return response;
        }
    }
}

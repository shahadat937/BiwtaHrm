using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.SiteVisit.Requests.Commands;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SiteVisit.Handlers.Commands
{
    public class DeclineSiteVisitCommandHandler: IRequestHandler<DeclineSiteVisitCommand,BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public DeclineSiteVisitCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(DeclineSiteVisitCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException("Invalid Request");
            }

            var sitevisit = await _unitOfWork.Repository<Hrm.Domain.SiteVisit>().Get(request.SiteVisitId);

            if (sitevisit == null)
            {
                throw new NotFoundException(nameof(sitevisit), request.SiteVisitId);
            }


            sitevisit.Status = "Declined";
            await _unitOfWork.Repository<Hrm.Domain.SiteVisit>().Update(sitevisit);
            await _unitOfWork.Save();

            var response = new BaseCommandResponse();
            response.Success = true;
            response.Message = "Site Visit Decline Successful";
            response.Id = sitevisit.SiteVisitId;

            return response;
        }
    }
}

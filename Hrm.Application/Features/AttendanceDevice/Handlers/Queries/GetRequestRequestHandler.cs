using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AttendanceDevice.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Queries
{
    public class GetRequestRequestHandler: IRequestHandler<GetRequestRequest,object>
    {
        private readonly IHrmRepository<Domain.AttDeviceCommands> _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public GetRequestRequestHandler(IUnitOfWork unitOfWork,  IMapper mapper, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<object> Handle(GetRequestRequest request, CancellationToken cancellationToken)
        {
            var IsAuthorizedDevice = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.SN == request.SN && x.Status == true).AnyAsync();

            if(!IsAuthorizedDevice)
            {
                return "Invalid Options";
            }

            var command = await _unitOfWork.Repository<Hrm.Domain.AttDeviceCommands>().Where(x => x.SN == request.SN).OrderBy(x => x.Id).FirstOrDefaultAsync();
            if(command==null)
            {
                return "OK\n";
            }

            var commandString = $"C:{command.Id}:{command.Command}\n";
            await _unitOfWork.Repository<Hrm.Domain.AttDeviceCommands>().Delete(command);
            await _unitOfWork.Save();

            return commandString;
            
        }
    }
}

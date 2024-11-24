using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AttendanceDevice.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Queries
{
    public class GetRequestRequestHandler: IRequestHandler<GetRequestRequest,object>
    {
        private readonly IHrmRepository<Domain.AttDeviceCommands> _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetRequestRequestHandler(IUnitOfWork unitOfWork,  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetRequestRequest request, CancellationToken cancellationToken)
        {
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

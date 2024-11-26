using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.AttendanceDevice.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Hrm.Application.Features.AttendanceDevice.Handlers.Queries
{
    public class PairDeviceRequestHandler:IRequestHandler<PairDeviceRequest,object>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContext;
        private readonly string UnauthorizedDeviceResponse = "Not Authorized Terminal";

        public PairDeviceRequestHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _httpContext = httpContext;
        }

        public async Task<object> Handle(PairDeviceRequest request, CancellationToken cancellationToken)
        {
            var pairedDevice = await _unitOfWork.Repository<Hrm.Domain.AttDevices>().Where(x => x.SN == request.SN).FirstOrDefaultAsync();
            var pendingDevice = await _unitOfWork.Repository<Hrm.Domain.PendingDevice>().Where(x => x.SN == request.SN).FirstOrDefaultAsync();
            if(pairedDevice == null && pendingDevice == null)
            {
                var device = new PendingDevice();
                device.Id = 0;
                device.SN = request.SN;
                device.DeviceType = request.DeviceType;
                device.DeviceIp = this.getDeviceIp();
                device.ExpireTime = DateTime.Now.AddSeconds(25);
                await _unitOfWork.Repository<Hrm.Domain.PendingDevice>().Add(device);
                await _unitOfWork.Save();
                return UnauthorizedDeviceResponse;
            } else if(pairedDevice == null && pendingDevice != null)
            {
                pendingDevice.ExpireTime = DateTime.Now.AddSeconds(25);
                await _unitOfWork.Repository<Hrm.Domain.PendingDevice>().Update(pendingDevice);
                await _unitOfWork.Save();
                return UnauthorizedDeviceResponse;
            }

            // Building the response for authorized device
            var parameters = await _unitOfWork.Repository<Hrm.Domain.DeviceParameters>().Where(x => true).ToListAsync();

            string defaultResponse = $"GET OPTIONS FROM: {pairedDevice.SN}\n";

            foreach(var parameter in parameters)
            {
                defaultResponse += $"{parameter.Name}={parameter.Value}\n";
            }

            // add other device associated parameters
            string stamp = pairedDevice.AttStamp == null ? "0" : pairedDevice.AttStamp;
            string OpStamp = pairedDevice.OpStamp == null ? "0" : pairedDevice.OpStamp;
            defaultResponse += $"TimeZone={pairedDevice.Timezone}\n";
            defaultResponse += $"Stamp={stamp}\n";
            defaultResponse += $"OpStamp={OpStamp}\n";

            return defaultResponse;
        }

        private string getDeviceIp()
        {
            string clientIp = _httpContext.HttpContext.Connection.RemoteIpAddress.ToString();

            var xForwardHeader = _httpContext.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (!string.IsNullOrEmpty(xForwardHeader))
            {
                clientIp = xForwardHeader.Split(',')[0];
            }

            return clientIp;
        }

    }
}

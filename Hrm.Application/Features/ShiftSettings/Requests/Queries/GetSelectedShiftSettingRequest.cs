using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.ShiftSettings.Requests.Queries
{
    public class GetSelectedShiftSettingRequest : IRequest<List<SelectedModel>>
    {
    }
}

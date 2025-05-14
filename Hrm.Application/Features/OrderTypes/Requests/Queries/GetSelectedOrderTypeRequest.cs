using Hrm.Application.DTOs.OrderType;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.OrderTypes.Requests.Queries
{
    public class GetSelectedOrderTypeRequest : IRequest<List<SelectedOrderTypeDto>>
    {
    }
}

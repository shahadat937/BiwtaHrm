﻿using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.ShiftTypes.Requests.Queries
{
    public class GetSelectedShiftTypeRequest : IRequest<List<SelectedModel>>
    {
    }
}

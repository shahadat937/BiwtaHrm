﻿using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.MaritalStatus.Requests.Queries
{
    public class GetSelectedMaritalStatusRequest : IRequest<List<SelectedModel>>
    {

    }
}

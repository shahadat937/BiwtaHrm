﻿using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.District.Requests.Queries
{
    public class GetSelectedDistrictRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      
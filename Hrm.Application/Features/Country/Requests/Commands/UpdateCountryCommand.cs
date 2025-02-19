﻿using Hrm.Application.DTOs.Country;
using Hrm.Application.DTOs.TrainingType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Country.Requests.Commands
{
    public class UpdateCountryCommand : IRequest<BaseCommandResponse>
    {
        public required CountryDto CountryDto { get; set; }
    }
}

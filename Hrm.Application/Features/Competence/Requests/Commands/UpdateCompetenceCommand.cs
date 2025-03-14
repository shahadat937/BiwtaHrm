﻿using Hrm.Application.DTOs.Competence;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Competence.Requests.Commands
{
    public class UpdateCompetenceCommand : IRequest<BaseCommandResponse>
    {
        public CompetenceDto CompetenceDto { get; set; }
    }
}

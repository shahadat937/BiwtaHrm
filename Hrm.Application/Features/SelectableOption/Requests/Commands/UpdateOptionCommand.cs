using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.SelectableOption;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SelectableOption.Requests.Commands
{
    public class UpdateOptionCommand: IRequest<BaseCommandResponse>
    {
        public SelectableOptionDto OptionDto { get; set; }
    }
}

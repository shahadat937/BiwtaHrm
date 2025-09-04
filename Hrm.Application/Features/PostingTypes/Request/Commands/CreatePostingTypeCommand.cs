using Hrm.Application.DTOs.PostingType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PostingTypes.Request.Commands
{
    public class CreatePostingTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreatePostingTypeDto PostingTypeDto { get; set; }
    }
}

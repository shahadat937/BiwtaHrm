using Hrm.Application.DTOs.TrainingType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TrainingType.Requests.Queries
{
    public class GetTrainingTypeByIdRequest : IRequest<TrainingTypeDto>
    {
        public int TrainingTypeId { get; set; }
    }
}

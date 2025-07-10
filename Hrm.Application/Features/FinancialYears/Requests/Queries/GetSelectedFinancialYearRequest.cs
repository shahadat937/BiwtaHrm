using Hrm.Application.DTOs.FinancialYear;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.FinancialYears.Requests.Queries
{
    public class GetSelectedFinancialYearRequest : IRequest<List<SelectedModel>>
    {
    }
}

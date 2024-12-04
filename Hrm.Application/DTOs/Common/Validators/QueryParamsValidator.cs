using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hrm.Application.DTOs.Common.Validators
{
    public class QueryParamsValidator : AbstractValidator<QueryParams>
    {
        
        public int[] allowedPageSize = { 5, 10, 15, 20, 25, 50, 100, 200, 500, 1000 };
        public QueryParamsValidator()
        {
            RuleFor(q => q.PageIndex).GreaterThanOrEqualTo(1);
            RuleFor(q => q.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSize.Contains(value))
                {
                    context.AddFailure("PageSize", $"Page size must be in { string.Join(",", allowedPageSize)}");
                }
            });
        }
    }
}

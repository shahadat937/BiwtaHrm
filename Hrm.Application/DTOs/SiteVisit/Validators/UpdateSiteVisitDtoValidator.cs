using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SiteVisit.Validators
{
    public class UpdateSiteVisitDtoValidator: AbstractValidator<SiteVisitDto>
    {
        //public UpdateSiteVisitDtoValidator() {
        //    Include(new ISiteVisitDtoValidator());
        //    RuleFor(data => data.SiteVisitId).NotEmpty().WithMessage("{PropertyName} is required");
        //}
    }
}

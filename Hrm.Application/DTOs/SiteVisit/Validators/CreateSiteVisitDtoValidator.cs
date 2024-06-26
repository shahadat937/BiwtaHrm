using FluentValidation;
using Hrm.Application.DTOs.Shift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SiteVisit.Validators
{
    public class CreateSiteVisitDtoValidator: AbstractValidator<CreateSiteVisitDto>
    {
        //public CreateSiteVisitDtoValidator() {
        //    Include(new ISiteVisitDtoValidator());
        //}
    }
}

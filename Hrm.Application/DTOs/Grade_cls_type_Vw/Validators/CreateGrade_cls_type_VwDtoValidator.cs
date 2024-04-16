using FluentValidation;
using Hrm.Application.DTOs.Grade_cls_type_Vw.Validators;
using Hrm.Application.DTOs.Grade_cls_type_Vw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Grade_cls_type_Vw.Validators
{

    public class CreateGrade_cls_type_VwDtoValidator : AbstractValidator<CreateGrade_cls_type_VwDto>
    {
        public CreateGrade_cls_type_VwDtoValidator()
        {
            Include(new IGrade_cls_type_VwDtoValidator());
        }
    }
}

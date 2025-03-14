﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.BloodGroup.Validators
{
    public class CreateBloodGroupDtoValidator : AbstractValidator<CreateBloodGroupDto>
    {
        public CreateBloodGroupDtoValidator()
        {
            Include(new IBloodGroupDtoValidator());
        }
    }
}

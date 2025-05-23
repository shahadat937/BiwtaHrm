﻿using Hrm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Domain
{
    public class Language:BaseDomainEntity
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<EmpLanguageInfo>? EmpLanguageInfo { get; set; }
    }
}

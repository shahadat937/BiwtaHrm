using Hrm.Application.DTOs.Upazila;
using Hrm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Upazila
{
    public class UpazilaDto : IUpazilaDto
    {
        public int UpazilaId { get; set; }
        public required string UpazilaName { get; set; }
        public int? DistrictId { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.DepReleaseInfo
{
    public interface IDepReleaseInfoDto
    {
        public int DepReleaseInfoId { get; set; }
        public string? DepReleaseInfoName { get; set; }
        public int? EmpId { get; set; }
        public string? OfficeOrderNo { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? OrderOfficeBy { get; set; }
        public string? ReferenceNo { get; set; }
        public string? DepClearance { get; set; }
        public string? ReleaseType { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}

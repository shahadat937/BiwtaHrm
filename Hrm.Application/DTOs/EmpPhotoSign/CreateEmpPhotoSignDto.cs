using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpPhotoSign
{
    public class CreateEmpPhotoSignDto : IEmpPhotoSignDto
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string PNo { get; set; }
        public string? PhotoUrl { get; set; }
        public IFormFile? PhotoFile { get; set; }
        public string? SignatureUrl { get; set; }
        public IFormFile? SignatureFile { get; set; }
        public string? UniqueIdentity { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool? IsActive { get; set; }
    }
}
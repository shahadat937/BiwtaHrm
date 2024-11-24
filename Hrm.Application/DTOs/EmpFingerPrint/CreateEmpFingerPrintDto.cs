using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpFingerPrint
{
    public class CreateEmpFingerPrintDto : IEmpFingerPrintDto
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string PNo { get; set; }
        public IFormFile? RightThumbFile { get; set; }
        public IFormFile? RightIndexFile { get; set; }
        public IFormFile? RightMiddleFile { get; set; }
        public IFormFile? RightRingFile { get; set; }
        public IFormFile? RightLittleFile { get; set; }
        public IFormFile? LeftThumbFile { get; set; }
        public IFormFile? LeftIndexFile { get; set; }
        public IFormFile? LeftMiddleFile { get; set; }
        public IFormFile? LeftRingFile { get; set; }
        public IFormFile? LeftLittleFile { get; set; }
        public string? RightThumb { get; set; }
        public string? RightIndex { get; set; }
        public string? RightMiddle { get; set; }
        public string? RightRing { get; set; }
        public string? RightLittle { get; set; }
        public string? LeftThumb { get; set; }
        public string? LeftIndex { get; set; }
        public string? LeftMiddle { get; set; }
        public string? LeftRing { get; set; }
        public string? LeftLittle { get; set; }
        public bool IsActive { get; set; }
    }
}

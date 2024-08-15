using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.DTOs.Features 
{
    public class CreateFeatureDto : IFeatureDto
    {
        public int FeatureId { get; set; }
        public int? ModuleId { get; set; }
        public int? FeatureTypeId { get; set; }
        public string? FeatureName { get; set; } = null!;
        public string? Path { get; set; } = null!;
        public string? IconName { get; set; }
        public string? Icon { get; set; }
        public string? Class { get; set; }
        public string? GroupTitle { get; set; }
        public bool IsActive { get; set; }
        public int? FeatureCode { get; set; }
        public int? OrderNo { get; set; }
        public bool IsReport { get; set; }
    }
}

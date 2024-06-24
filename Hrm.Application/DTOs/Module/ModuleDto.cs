
namespace Hrm.Application.DTOs.Modules
{
    public class ModuleDto : IModuleDto
    {


        public int ModuleId { get; set; }
        public string? Title { get; set; }
        public string? ModuleName { get; set; }
        public string? IconName { get; set; }
        public string? Icon { get; set; }
        public string? Class { get; set; }
        public string? GroupTitle { get; set; }
        public int? Status { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        
    }
}


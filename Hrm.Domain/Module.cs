using Hrm.Domain.Common;

namespace Hrm.Domain
{
    public class Module : BaseDomainEntity
    {
        public Module()
        {
            Features = new HashSet<Feature>();
        }

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

        public virtual ICollection<Feature> Features { get; set; }

    }
}

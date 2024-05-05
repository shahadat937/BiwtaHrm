using Hrm.Domain.Common;


namespace Hrm.Domain
{
    public class Designation: BaseDomainEntity
    {
        public int DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}

using Hrm.Domain.Common;


namespace Hrm.Domain
{
    public class Designation: BaseDomainEntity
    {
        public int DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public string? DesignationNameBangla { get; set; }
        public int? OfficeId { get; set; }
        public int? DepartmentId { get; set; }
        public string? Remark { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public Office Office { get; set; }
        public Department Department { get; set; }
    }
}

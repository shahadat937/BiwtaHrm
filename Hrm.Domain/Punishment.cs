using Hrm.Domain.Common;

namespace Hrm.Domain
{
    public class Punishment : BaseDomainEntity
    {
        public int PunishmentId { get; set; }
        public string? PunishmentName { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }
    }
}
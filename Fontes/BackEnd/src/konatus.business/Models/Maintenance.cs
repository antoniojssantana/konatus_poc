using konatus.business.Enums;
using System;

namespace konatus.business.Models
{
    public class MaintenanceModel : Entity
    {
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public StatusMaintenance StatusMaintenance { get; set; }
    }
}
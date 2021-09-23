using konatus.business.Enums;
using System;

namespace konatus.business.Models
{
    public class StageModel : Entity
    {
        public Guid MaintenanceId { get; set; }
        public MaintenanceModel Maintenance { get; set; }
        public string Description { get; set; }
        public StatusStage StatusStage { get; set; }
        public StageType Type { get; set; }
        public string Value { get; set; }
    }
}
using System;

namespace konatus.api.ViewModels
{
    public class StageViewModel
    {
        public Guid Id { get; set; }
        public Guid MaintenanceId { get; set; }
        public string Description { get; set; }
        public int? StatusStage { get; set; }
        public int? Type { get; set; }
        public string? Value { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
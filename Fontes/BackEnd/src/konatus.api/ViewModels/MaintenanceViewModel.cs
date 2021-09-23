using System;

namespace konatus.api.ViewModels
{
    public class MaintenanceViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public int? StatusMaintenance { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
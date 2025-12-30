// CRM_API/Models/MaintenanceVM.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace CRM_API.Models
{
    public class MaintenanceVM
    {
        public int Id { get; set; }

        [Required]
        public int VehicleId { get; set; }

        public string VehiclePlateNumber { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }

        [Required]
        [StringLength(100)]
        public string MaintenanceType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public int? CurrentMileage { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }

        public decimal? ActualCost { get; set; }

        [Required]
        public string Status { get; set; }

        public string MechanicName { get; set; }

        public string MechanicPhone { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsActive { get; set; }

        public bool IsOverdue => Status == "Scheduled" && ScheduledDate < DateTime.Today;
    }

    public class MaintenanceRequest
    {
        [Required]
        public int VehicleId { get; set; }

        [Required]
        [StringLength(100)]
        public string MaintenanceType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime ScheduledDate { get; set; }

        public int? CurrentMileage { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }

        public string MechanicName { get; set; }

        public string MechanicPhone { get; set; }

        public string Notes { get; set; }
    }

    public class CompleteMaintenanceRequest
    {
        [Required]
        public DateTime CompletionDate { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? ActualCost { get; set; }

        public string Notes { get; set; }
    }

    public class CancelMaintenanceRequest
    {
        public string Reason { get; set; }
    }
}
// CRMdataLayer/Entities/Maintenance.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMdataLayer.Entities
{
    [Table("Maintenances")]
    public class Maintenance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VehicleId { get; set; }

        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }

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
        [Column(TypeName = "decimal(18,2)")]
        public decimal EstimatedCost { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ActualCost { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Scheduled"; // Scheduled, In Progress, Completed, Cancelled

        [StringLength(100)]
        public string MechanicName { get; set; }
        [StringLength(100)]
        public string? CreatedBy { get; set; }

        [StringLength(20)]
        public string MechanicPhone { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
    }
}
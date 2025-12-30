using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car_Rental_Management_System.Entities
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        [StringLength(100)]
        public string? ReportType { get; set; }

        [Required]
        public DateTime GeneratedDate { get; set; }

        [StringLength(500)]
        public string? Parameters { get; set; }

        public int TotalRecords { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [StringLength(1000)]
        public string? FilePath { get; set; }

        public DateTime? ExportedDate { get; set; }

        public string? GeneratedBy { get; set; }
    }

    // If you need view entities for reports
    public class RentalReportView
    {
        public int RentalId { get; set; }
        public string? CustomerName { get; set; }
        public string? VehicleName { get; set; }
        public string? VehicleType { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }
        public int DaysRented { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }
    }

    public class CustomerReportView
    {
        public int CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int TotalRentals { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
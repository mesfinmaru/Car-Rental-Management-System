// CRMdataLayer.Entities namespace
using System;

namespace CRMdataLayer.Entities
{
    public class Vehicle // Changed from Vehicles to Vehicle (singular)
    {
        public int Id { get; set; }

        // Basic Information
        public string? PlateNumber { get; set; }          // Required, Unique
        public string? Make { get; set; }                 // Required
        public string? Model { get; set; }                // Required
        public int Year { get; set; }                    // Required
        public string? Color { get; set; }

        // Vehicle Details
        public string? VehicleType { get; set; }          // Sedan, SUV, Truck, Van, etc.
        public string? Transmission { get; set; }         // Automatic, Manual
        public string? FuelType { get; set; }             // Petrol, Diesel, Electric, Hybrid
        public int Mileage { get; set; }                 // Current mileage
        public int Seats { get; set; }                   // Number of seats
        public string? Features { get; set; }             // JSON or comma-separated

        // Rental Information
        public decimal DailyRate { get; set; }           // Required
        public decimal WeeklyRate { get; set; }
        public decimal MonthlyRate { get; set; }
        public bool IsAvailable { get; set; }            // For rental availability
        public bool IsActive { get; set; }               // For system status

        // Additional Info
        public string? VIN { get; set; }                  // Vehicle Identification Number
        public string? EngineNumber { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public DateTime? NextServiceDate { get; set; }
        public string? Notes { get; set; }

        // System
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public string? Status { get; set; }
        // In Vehicle.cs, add this property:
        public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();
    }
}
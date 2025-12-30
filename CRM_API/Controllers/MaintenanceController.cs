// CRM_API/Controllers/MaintenanceController.cs
using CRM_API.Models;
using CRMdataLayer;
using CRMdataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MaintenancesController : ControllerBase
    {
        private readonly AppDBContext _context;

        public MaintenancesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/maintenances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaintenanceVM>>> GetMaintenances(
            string search = null,
            string status = null)
        {
            try
            {
                var query = _context.Maintenances
                    .Include(m => m.Vehicle)
                    .Where(m => m.IsActive)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(m =>
                        m.MaintenanceType.Contains(search) ||
                        m.Description.Contains(search) ||
                        m.MechanicName.Contains(search) ||
                        m.Vehicle.PlateNumber.Contains(search) ||
                        m.Vehicle.Make.Contains(search) ||
                        m.Vehicle.Model.Contains(search));
                }

                if (!string.IsNullOrWhiteSpace(status) && status != "All")
                {
                    query = query.Where(m => m.Status == status);
                }

                var maintenances = await query
                    .OrderByDescending(m => m.ScheduledDate)
                    .Select(m => new MaintenanceVM
                    {
                        Id = m.Id,
                        VehicleId = m.VehicleId,
                        VehiclePlateNumber = m.Vehicle.PlateNumber,
                        VehicleMake = m.Vehicle.Make,
                        VehicleModel = m.Vehicle.Model,
                        MaintenanceType = m.MaintenanceType,
                        Description = m.Description,
                        ScheduledDate = m.ScheduledDate,
                        StartDate = m.StartDate,
                        CompletionDate = m.CompletionDate,
                        CurrentMileage = m.CurrentMileage,
                        Cost = m.EstimatedCost,
                        ActualCost = m.ActualCost,
                        Status = m.Status,
                        MechanicName = m.MechanicName,
                        MechanicPhone = m.MechanicPhone,
                        Notes = m.Notes,
                        CreatedAt = m.CreatedAt,
                        IsActive = m.IsActive
                    })
                    .ToListAsync();

                return Ok(maintenances);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/maintenances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaintenanceVM>> GetMaintenance(int id)
        {
            try
            {
                var maintenance = await _context.Maintenances
                    .Include(m => m.Vehicle)
                    .FirstOrDefaultAsync(m => m.Id == id && m.IsActive);

                if (maintenance == null)
                {
                    return NotFound($"Maintenance with ID {id} not found");
                }

                var maintenanceVM = new MaintenanceVM
                {
                    Id = maintenance.Id,
                    VehicleId = maintenance.VehicleId,
                    VehiclePlateNumber = maintenance.Vehicle.PlateNumber,
                    VehicleMake = maintenance.Vehicle.Make,
                    VehicleModel = maintenance.Vehicle.Model,
                    MaintenanceType = maintenance.MaintenanceType,
                    Description = maintenance.Description,
                    ScheduledDate = maintenance.ScheduledDate,
                    StartDate = maintenance.StartDate,
                    CompletionDate = maintenance.CompletionDate,
                    CurrentMileage = maintenance.CurrentMileage,
                    Cost = maintenance.EstimatedCost,
                    ActualCost = maintenance.ActualCost,
                    Status = maintenance.Status,
                    MechanicName = maintenance.MechanicName,
                    MechanicPhone = maintenance.MechanicPhone,
                    Notes = maintenance.Notes,
                    CreatedAt = maintenance.CreatedAt,
                    IsActive = maintenance.IsActive
                };

                return Ok(maintenanceVM);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/maintenances
        [HttpPost]
        public async Task<ActionResult<MaintenanceVM>> CreateMaintenance(MaintenanceRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verify vehicle exists and is available
                var vehicle = await _context.Vehicles.FindAsync(request.VehicleId);
                if (vehicle == null)
                {
                    return BadRequest("Vehicle not found");
                }

                if (!vehicle.IsActive)
                {
                    return BadRequest("Vehicle is not active");
                }

                var maintenance = new Maintenance
                {
                    VehicleId = request.VehicleId,
                    MaintenanceType = request.MaintenanceType,
                    Description = request.Description,
                    ScheduledDate = request.ScheduledDate,
                    CurrentMileage = request.CurrentMileage,
                    EstimatedCost = request.Cost,
                    Status = "Scheduled",
                    MechanicName = request.MechanicName,
                    MechanicPhone = request.MechanicPhone,
                    Notes = request.Notes,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                _context.Maintenances.Add(maintenance);
                await _context.SaveChangesAsync();

                // Mark vehicle as unavailable
                vehicle.IsAvailable = false;
                _context.Vehicles.Update(vehicle);
                await _context.SaveChangesAsync();

                var maintenanceVM = new MaintenanceVM
                {
                    Id = maintenance.Id,
                    VehicleId = maintenance.VehicleId,
                    MaintenanceType = maintenance.MaintenanceType,
                    Description = maintenance.Description,
                    ScheduledDate = maintenance.ScheduledDate,
                    CurrentMileage = maintenance.CurrentMileage,
                    Cost = maintenance.EstimatedCost,
                    Status = maintenance.Status,
                    MechanicName = maintenance.MechanicName,
                    MechanicPhone = maintenance.MechanicPhone,
                    Notes = maintenance.Notes,
                    CreatedAt = maintenance.CreatedAt,
                    IsActive = maintenance.IsActive
                };

                return CreatedAtAction(nameof(GetMaintenance), new { id = maintenance.Id }, maintenanceVM);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/maintenances/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaintenance(int id, MaintenanceVM maintenanceVM)
        {
            try
            {
                if (id != maintenanceVM.Id)
                {
                    return BadRequest("ID mismatch");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var maintenance = await _context.Maintenances.FindAsync(id);
                if (maintenance == null || !maintenance.IsActive)
                {
                    return NotFound($"Maintenance with ID {id} not found");
                }

                // Cannot edit if in progress or completed
                if (maintenance.Status == "In Progress" || maintenance.Status == "Completed")
                {
                    return BadRequest($"Cannot edit {maintenance.Status} maintenance records");
                }

                maintenance.MaintenanceType = maintenanceVM.MaintenanceType;
                maintenance.Description = maintenanceVM.Description;
                maintenance.ScheduledDate = maintenanceVM.ScheduledDate;
                maintenance.CurrentMileage = maintenanceVM.CurrentMileage;
                maintenance.EstimatedCost = maintenanceVM.Cost;
                maintenance.MechanicName = maintenanceVM.MechanicName;
                maintenance.MechanicPhone = maintenanceVM.MechanicPhone;
                maintenance.Notes = maintenanceVM.Notes;
                maintenance.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/maintenances/5/start
        [HttpPut("{id}/start")]
        public async Task<IActionResult> StartMaintenance(int id)
        {
            try
            {
                var maintenance = await _context.Maintenances.FindAsync(id);
                if (maintenance == null || !maintenance.IsActive)
                {
                    return NotFound($"Maintenance with ID {id} not found");
                }

                if (maintenance.Status != "Scheduled")
                {
                    return BadRequest("Only scheduled maintenance records can be started");
                }

                maintenance.Status = "In Progress";
                maintenance.StartDate = DateTime.UtcNow;
                maintenance.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/maintenances/5/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteMaintenance(int id, CompleteMaintenanceRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var maintenance = await _context.Maintenances.FindAsync(id);
                if (maintenance == null || !maintenance.IsActive)
                {
                    return NotFound($"Maintenance with ID {id} not found");
                }

                if (maintenance.Status != "In Progress")
                {
                    return BadRequest("Only maintenance records in progress can be completed");
                }

                maintenance.Status = "Completed";
                maintenance.CompletionDate = request.CompletionDate;
                maintenance.ActualCost = request.ActualCost;
                if (!string.IsNullOrEmpty(request.Notes))
                {
                    maintenance.Notes += $"\n\nCompletion Notes: {request.Notes}";
                }
                maintenance.UpdatedAt = DateTime.UtcNow;

                // Mark vehicle as available again
                var vehicle = await _context.Vehicles.FindAsync(maintenance.VehicleId);
                if (vehicle != null)
                {
                    vehicle.IsAvailable = true;
                    _context.Vehicles.Update(vehicle);
                }

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/maintenances/5/cancel
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelMaintenance(int id, [FromQuery] string reason = null)
        {
            try
            {
                var maintenance = await _context.Maintenances.FindAsync(id);
                if (maintenance == null || !maintenance.IsActive)
                {
                    return NotFound($"Maintenance with ID {id} not found");
                }

                if (maintenance.Status == "Completed")
                {
                    return BadRequest("Cannot cancel completed maintenance records");
                }

                maintenance.Status = "Cancelled";
                maintenance.UpdatedAt = DateTime.UtcNow;

                if (!string.IsNullOrEmpty(reason))
                {
                    maintenance.Notes += $"\n\nCancellation Reason: {reason}";
                }

                // Mark vehicle as available again
                var vehicle = await _context.Vehicles.FindAsync(maintenance.VehicleId);
                if (vehicle != null)
                {
                    vehicle.IsAvailable = true;
                    _context.Vehicles.Update(vehicle);
                }

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/maintenances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenance(int id)
        {
            try
            {
                var maintenance = await _context.Maintenances.FindAsync(id);
                if (maintenance == null || !maintenance.IsActive)
                {
                    return NotFound($"Maintenance with ID {id} not found");
                }

                if (maintenance.Status == "In Progress" || maintenance.Status == "Completed")
                {
                    return BadRequest($"Cannot delete {maintenance.Status} maintenance records");
                }

                maintenance.IsActive = false;
                maintenance.UpdatedAt = DateTime.UtcNow;

                // Mark vehicle as available again
                var vehicle = await _context.Vehicles.FindAsync(maintenance.VehicleId);
                if (vehicle != null)
                {
                    vehicle.IsAvailable = true;
                    _context.Vehicles.Update(vehicle);
                }

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/maintenances/vehicle/{vehicleId}
        [HttpGet("vehicle/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<MaintenanceVM>>> GetVehicleMaintenances(int vehicleId)
        {
            try
            {
                var maintenances = await _context.Maintenances
                    .Include(m => m.Vehicle)
                    .Where(m => m.VehicleId == vehicleId && m.IsActive)
                    .OrderByDescending(m => m.ScheduledDate)
                    .Select(m => new MaintenanceVM
                    {
                        Id = m.Id,
                        VehicleId = m.VehicleId,
                        VehiclePlateNumber = m.Vehicle.PlateNumber,
                        VehicleMake = m.Vehicle.Make,
                        VehicleModel = m.Vehicle.Model,
                        MaintenanceType = m.MaintenanceType,
                        Description = m.Description,
                        ScheduledDate = m.ScheduledDate,
                        StartDate = m.StartDate,
                        CompletionDate = m.CompletionDate,
                        CurrentMileage = m.CurrentMileage,
                        Cost = m.EstimatedCost,
                        ActualCost = m.ActualCost,
                        Status = m.Status,
                        MechanicName = m.MechanicName,
                        MechanicPhone = m.MechanicPhone,
                        Notes = m.Notes,
                        CreatedAt = m.CreatedAt,
                        IsActive = m.IsActive
                    })
                    .ToListAsync();

                return Ok(maintenances);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
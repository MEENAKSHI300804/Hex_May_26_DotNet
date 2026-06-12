using FleetMaintenanceApi.DTOs;
using FleetMaintenanceApi.Models;
using FleetMaintenanceApi.Repositories.Interfaces;
using FleetMaintenanceApi.Services.Interfaces;

namespace FleetMaintenanceApi.Services.Implementations;

public class MaintenanceService : IMaintenanceService
{
    private readonly IMaintenanceRepository _repository;

    public MaintenanceService(IMaintenanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MaintenanceResponseDto>> GetAllAsync()
    {
        var records = await _repository.GetAllAsync();

        return records.Select(r => new MaintenanceResponseDto
        {
            MaintenanceId = r.MaintenanceId,
            VehicleId = r.VehicleId,
            DriverId = r.DriverId,
            ServiceDate = r.ServiceDate,
            ServiceType = r.ServiceType,
            ServiceCost = r.ServiceCost,
            ServiceStatus = r.ServiceStatus,
            Remarks = r.Remarks,
            CreatedDate = r.CreatedDate
        });
    }

    public async Task<MaintenanceResponseDto?> GetByIdAsync(int id)
    {
        var record = await _repository.GetByIdAsync(id);

        if (record == null)
            return null;

        return new MaintenanceResponseDto
        {
            MaintenanceId = record.MaintenanceId,
            VehicleId = record.VehicleId,
            DriverId = record.DriverId,
            ServiceDate = record.ServiceDate,
            ServiceType = record.ServiceType,
            ServiceCost = record.ServiceCost,
            ServiceStatus = record.ServiceStatus,
            Remarks = record.Remarks,
            CreatedDate = record.CreatedDate
        };
    }

    public async Task<MaintenanceResponseDto> CreateAsync(MaintenanceCreateDto dto)
    {
        var record = new MaintenanceRecord
        {
            VehicleId = dto.VehicleId,
            DriverId = dto.DriverId,
            ServiceDate = dto.ServiceDate,
            ServiceType = dto.ServiceType,
            ServiceCost = dto.ServiceCost,
            ServiceStatus = dto.ServiceStatus,
            Remarks = dto.Remarks,
            CreatedDate = DateTime.Now
        };

        await _repository.AddAsync(record);

        return new MaintenanceResponseDto
        {
            MaintenanceId = record.MaintenanceId,
            VehicleId = record.VehicleId,
            DriverId = record.DriverId,
            ServiceDate = record.ServiceDate,
            ServiceType = record.ServiceType,
            ServiceCost = record.ServiceCost,
            ServiceStatus = record.ServiceStatus,
            Remarks = record.Remarks,
            CreatedDate = record.CreatedDate
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var record = await _repository.GetByIdAsync(id);

        if (record == null)
            return false;

        await _repository.DeleteAsync(record);

        return true;
    }
}
using FleetMaintenanceApi.DTOs;
using FleetMaintenanceApi.Models;
using FleetMaintenanceApi.Repositories.Interfaces;
using FleetMaintenanceApi.Services.Interfaces;

namespace FleetMaintenanceApi.Services.Implementations;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _repository;

    public VehicleService(IVehicleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<VehicleResponseDto>> GetAllAsync()
    {
        var vehicles = await _repository.GetAllAsync();

        return vehicles.Select(v => new VehicleResponseDto
        {
            VehicleId = v.VehicleId,
            VehicleNumber = v.VehicleNumber,
            VehicleType = v.VehicleType,
            Brand = v.Brand,
            Model = v.Model,
            PurchaseYear = v.PurchaseYear,
            IsActive = v.IsActive
        });
    }

    public async Task<VehicleResponseDto?> GetByIdAsync(int id)
    {
        var vehicle = await _repository.GetByIdAsync(id);

        if (vehicle == null)
            return null;

        return new VehicleResponseDto
        {
            VehicleId = vehicle.VehicleId,
            VehicleNumber = vehicle.VehicleNumber,
            VehicleType = vehicle.VehicleType,
            Brand = vehicle.Brand,
            Model = vehicle.Model,
            PurchaseYear = vehicle.PurchaseYear,
            IsActive = vehicle.IsActive
        };
    }

    public async Task<VehicleResponseDto> CreateAsync(VehicleCreateDto dto)
    {
        var vehicle = new Vehicle
        {
            VehicleNumber = dto.VehicleNumber,
            VehicleType = dto.VehicleType,
            Brand = dto.Brand,
            Model = dto.Model,
            PurchaseYear = dto.PurchaseYear,
            IsActive = dto.IsActive
        };

        await _repository.AddAsync(vehicle);

        return new VehicleResponseDto
        {
            VehicleId = vehicle.VehicleId,
            VehicleNumber = vehicle.VehicleNumber,
            VehicleType = vehicle.VehicleType,
            Brand = vehicle.Brand,
            Model = vehicle.Model,
            PurchaseYear = vehicle.PurchaseYear,
            IsActive = vehicle.IsActive
        };
    }

    public async Task<bool> UpdateAsync(int id, VehicleCreateDto dto)
    {
        var vehicle = await _repository.GetByIdAsync(id);

        if (vehicle == null)
            return false;

        vehicle.VehicleNumber = dto.VehicleNumber;
        vehicle.VehicleType = dto.VehicleType;
        vehicle.Brand = dto.Brand;
        vehicle.Model = dto.Model;
        vehicle.PurchaseYear = dto.PurchaseYear;
        vehicle.IsActive = dto.IsActive;

        await _repository.UpdateAsync(vehicle);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var vehicle = await _repository.GetByIdAsync(id);

        if (vehicle == null)
            return false;

        await _repository.DeleteAsync(vehicle);

        return true;
    }
}
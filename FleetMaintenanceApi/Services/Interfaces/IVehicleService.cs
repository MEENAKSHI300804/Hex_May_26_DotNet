using FleetMaintenanceApi.DTOs;

namespace FleetMaintenanceApi.Services.Interfaces;

public interface IVehicleService
{
    Task<IEnumerable<VehicleResponseDto>> GetAllAsync();

    Task<VehicleResponseDto?> GetByIdAsync(int id);

    Task<VehicleResponseDto> CreateAsync(VehicleCreateDto dto);

    Task<bool> UpdateAsync(int id, VehicleCreateDto dto);

    Task<bool> DeleteAsync(int id);
}
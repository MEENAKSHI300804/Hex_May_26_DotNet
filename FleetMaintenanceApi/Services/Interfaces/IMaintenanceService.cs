using FleetMaintenanceApi.DTOs;

namespace FleetMaintenanceApi.Services.Interfaces;

public interface IMaintenanceService
{
    Task<IEnumerable<MaintenanceResponseDto>> GetAllAsync();

    Task<MaintenanceResponseDto?> GetByIdAsync(int id);

    Task<MaintenanceResponseDto> CreateAsync(MaintenanceCreateDto dto);

    Task<bool> DeleteAsync(int id);
}
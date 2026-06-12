using FleetMaintenanceApi.DTOs;

namespace FleetMaintenanceApi.Services.Interfaces;

public interface IDriverService
{
    Task<IEnumerable<DriverResponseDto>> GetAllAsync();

    Task<DriverResponseDto?> GetByIdAsync(int id);

    Task<DriverResponseDto> CreateAsync(DriverCreateDto dto);

    Task<bool> UpdateAsync(int id, DriverCreateDto dto);

    Task<bool> DeleteAsync(int id);
}
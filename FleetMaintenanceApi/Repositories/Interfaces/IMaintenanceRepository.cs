using FleetMaintenanceApi.Models;

namespace FleetMaintenanceApi.Repositories.Interfaces;

public interface IMaintenanceRepository
{
    Task<IEnumerable<MaintenanceRecord>> GetAllAsync();

    Task<MaintenanceRecord?> GetByIdAsync(int id);

    Task<MaintenanceRecord> AddAsync(MaintenanceRecord record);

    Task UpdateAsync(MaintenanceRecord record);

    Task DeleteAsync(MaintenanceRecord record);
}
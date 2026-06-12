using FleetMaintenanceApi.Models;

namespace FleetMaintenanceApi.Repositories.Interfaces;

public interface IDriverRepository
{
    Task<IEnumerable<Driver>> GetAllAsync();

    Task<Driver?> GetByIdAsync(int id);

    Task<Driver> AddAsync(Driver driver);

    Task UpdateAsync(Driver driver);

    Task DeleteAsync(Driver driver);
}
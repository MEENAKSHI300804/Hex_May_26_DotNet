using FleetMaintenanceApi.Models;

namespace FleetMaintenanceApi.Repositories.Interfaces;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetAllAsync();

    Task<Vehicle?> GetByIdAsync(int id);

    Task<Vehicle> AddAsync(Vehicle vehicle);

    Task UpdateAsync(Vehicle vehicle);

    Task DeleteAsync(Vehicle vehicle);
}
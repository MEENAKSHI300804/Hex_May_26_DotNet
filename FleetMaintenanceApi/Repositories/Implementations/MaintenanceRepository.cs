using FleetMaintenanceApi.Data;
using FleetMaintenanceApi.Models;
using FleetMaintenanceApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetMaintenanceApi.Repositories.Implementations;

public class MaintenanceRepository : IMaintenanceRepository
{
    private readonly FleetMaintenanceDbContext _context;

    public MaintenanceRepository(FleetMaintenanceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MaintenanceRecord>> GetAllAsync()
    {
        return await _context.MaintenanceRecords
            .Include(x => x.Vehicle)
            .Include(x => x.Driver)
            .ToListAsync();
    }

    public async Task<MaintenanceRecord?> GetByIdAsync(int id)
    {
        return await _context.MaintenanceRecords
            .Include(x => x.Vehicle)
            .Include(x => x.Driver)
            .FirstOrDefaultAsync(x => x.MaintenanceId == id);
    }

    public async Task<MaintenanceRecord> AddAsync(MaintenanceRecord record)
    {
        _context.MaintenanceRecords.Add(record);

        await _context.SaveChangesAsync();

        return record;
    }

    public async Task UpdateAsync(MaintenanceRecord record)
    {
        _context.MaintenanceRecords.Update(record);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(MaintenanceRecord record)
    {
        _context.MaintenanceRecords.Remove(record);

        await _context.SaveChangesAsync();
    }
}
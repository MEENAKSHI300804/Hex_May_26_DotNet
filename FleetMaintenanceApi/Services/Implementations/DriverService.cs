using FleetMaintenanceApi.DTOs;
using FleetMaintenanceApi.Models;
using FleetMaintenanceApi.Repositories.Interfaces;
using FleetMaintenanceApi.Services.Interfaces;

namespace FleetMaintenanceApi.Services.Implementations;

public class DriverService : IDriverService
{
    private readonly IDriverRepository _repository;

    public DriverService(IDriverRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DriverResponseDto>> GetAllAsync()
    {
        var drivers = await _repository.GetAllAsync();

        return drivers.Select(d => new DriverResponseDto
        {
            DriverId = d.DriverId,
            DriverName = d.DriverName,
            LicenseNumber = d.LicenseNumber,
            PhoneNumber = d.PhoneNumber,
            City = d.City,
            IsAvailable = d.IsAvailable
        });
    }

    public async Task<DriverResponseDto?> GetByIdAsync(int id)
    {
        var driver = await _repository.GetByIdAsync(id);

        if (driver == null)
            return null;

        return new DriverResponseDto
        {
            DriverId = driver.DriverId,
            DriverName = driver.DriverName,
            LicenseNumber = driver.LicenseNumber,
            PhoneNumber = driver.PhoneNumber,
            City = driver.City,
            IsAvailable = driver.IsAvailable
        };
    }

    public async Task<DriverResponseDto> CreateAsync(DriverCreateDto dto)
    {
        var driver = new Driver
        {
            DriverName = dto.DriverName,
            LicenseNumber = dto.LicenseNumber,
            PhoneNumber = dto.PhoneNumber,
            City = dto.City,
            IsAvailable = dto.IsAvailable
        };

        await _repository.AddAsync(driver);

        return new DriverResponseDto
        {
            DriverId = driver.DriverId,
            DriverName = driver.DriverName,
            LicenseNumber = driver.LicenseNumber,
            PhoneNumber = driver.PhoneNumber,
            City = driver.City,
            IsAvailable = driver.IsAvailable
        };
    }

    public async Task<bool> UpdateAsync(int id, DriverCreateDto dto)
    {
        var driver = await _repository.GetByIdAsync(id);

        if (driver == null)
            return false;

        driver.DriverName = dto.DriverName;
        driver.LicenseNumber = dto.LicenseNumber;
        driver.PhoneNumber = dto.PhoneNumber;
        driver.City = dto.City;
        driver.IsAvailable = dto.IsAvailable;

        await _repository.UpdateAsync(driver);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var driver = await _repository.GetByIdAsync(id);

        if (driver == null)
            return false;

        await _repository.DeleteAsync(driver);

        return true;
    }
}
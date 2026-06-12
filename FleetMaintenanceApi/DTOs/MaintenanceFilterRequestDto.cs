namespace FleetMaintenanceApi.DTOs;

public class MaintenanceFilterRequestDto
{
    public string? ServiceStatus { get; set; }

    public string? ServiceType { get; set; }

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 5;
}
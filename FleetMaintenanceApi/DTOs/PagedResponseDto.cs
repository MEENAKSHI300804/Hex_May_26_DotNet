namespace FleetMaintenanceApi.DTOs;

public class PagedResponseDto<T>
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalRecords { get; set; }

    public IEnumerable<T> Data { get; set; } = new List<T>();
}
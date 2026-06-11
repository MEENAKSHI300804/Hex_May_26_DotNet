using EmployeeLeaveRequestAPI.DTOs;

namespace EmployeeLeaveRequestAPI.Services
{
    public interface ILeaveRequestService
    {
        LeaveRequestResponseDto Create(
            LeaveRequestCreateDto dto);

        List<LeaveRequestResponseDto> GetAll();

        LeaveRequestResponseDto GetById(int id);
    }
}
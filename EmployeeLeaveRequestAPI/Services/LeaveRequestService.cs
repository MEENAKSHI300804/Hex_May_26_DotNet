using EmployeeLeaveRequestAPI.DTOs;
using EmployeeLeaveRequestAPI.Models;

namespace EmployeeLeaveRequestAPI.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private static List<LeaveRequest> requests =
            new List<LeaveRequest>();

        private static int id = 1;

        public LeaveRequestResponseDto Create(
            LeaveRequestCreateDto dto)
        {
            if (dto.StartDate.Date <= DateTime.Today)
            {
                throw new Exception(
                    "Start Date must be a future date");
            }

            if (dto.EndDate.Date <= DateTime.Today)
            {
                throw new Exception(
                    "End Date must be a future date");
            }

            if (dto.EndDate < dto.StartDate)
            {
                throw new Exception(
                    "End Date must be after Start Date");
            }

            LeaveRequest request = new LeaveRequest
            {
                LeaveRequestId = id++,

                EmployeeName = dto.EmployeeName,

                EmployeeEmail = dto.EmployeeEmail,

                MobileNumber = dto.MobileNumber,

                LeaveType = dto.LeaveType,

                StartDate = dto.StartDate,

                EndDate = dto.EndDate,

                Reason = dto.Reason,

                TotalDays =
                    (dto.EndDate - dto.StartDate).Days + 1,

                Status = "Pending",

                CreatedOn = DateTime.Now
            };

            requests.Add(request);

            return Map(request);
        }

        public List<LeaveRequestResponseDto> GetAll()
        {
            return requests
                .Select(Map)
                .ToList();
        }

        public LeaveRequestResponseDto GetById(int id)
        {
            var request =
                requests.FirstOrDefault(x =>
                x.LeaveRequestId == id);

            if (request == null)
                return null;

            return Map(request);
        }

        private LeaveRequestResponseDto Map(
            LeaveRequest request)
        {
            return new LeaveRequestResponseDto
            {
                LeaveRequestId = request.LeaveRequestId,
                EmployeeName = request.EmployeeName,
                EmployeeEmail = request.EmployeeEmail,
                LeaveType = request.LeaveType,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Reason = request.Reason,
                TotalDays = request.TotalDays,
                Status = request.Status,
                CreatedOn = request.CreatedOn
            };
        }
    }
}
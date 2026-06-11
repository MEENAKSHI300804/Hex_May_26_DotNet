using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveRequestAPI.Validation
{
    public class ValidLeaveTypeAttribute : ValidationAttribute
    {
        private readonly string[] allowedTypes =
        {
            "Sick",
            "Casual",
            "Earned"
        };

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Leave Type is required");
            }

            string leaveType = value.ToString();

            if (!allowedTypes.Contains(leaveType))
            {
                return new ValidationResult(
                    "Leave Type must be Sick, Casual or Earned");
            }

            return ValidationResult.Success;
        }
    }
}
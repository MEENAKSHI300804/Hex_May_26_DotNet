using Microsoft.AspNetCore.Mvc;
using EmployeeLeaveRequestAPI.DTOs;
using EmployeeLeaveRequestAPI.Services;

namespace EmployeeLeaveRequestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly ILeaveRequestService service;

        public LeaveRequestsController(
            ILeaveRequestService service)
        {
            this.service = service;
        }

        [HttpPost]
        public IActionResult Create(
            LeaveRequestCreateDto dto)
        {
            try
            {
                var result =
                    service.Create(dto);

                return Created("", result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var request =
                service.GetById(id);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using xUnit_testing_REST_api.Models;
using xUnit_testing_REST_api.Services.Interfaces;

namespace xUnit_testing_REST_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDetailsController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeDetailsController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetEmployees();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            var createdEmployee = await _employeeService.AddEmployee(employee);
            return Ok(createdEmployee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (employee == null || id != employee.EmployeeId)
            {
                return BadRequest();
            }
            var updatedEmployee = await _employeeService.UpdateEmployee(id, employee);
            if (updatedEmployee == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

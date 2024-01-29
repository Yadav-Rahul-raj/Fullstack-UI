using Microsoft.AspNetCore.Mvc;
using Employee_API.Data;
using Microsoft.EntityFrameworkCore;
using Employee_API.Models;


namespace Employee_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext _employee;
        public EmployeeController(EmployeeDbContext employee)
        {
            _employee = employee;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
           var employees =  await _employee.Employees.ToListAsync();
            return Ok(employees);
        }
        
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddEmplouee([FromBody]Employee employee)
        {
            employee.Id = Guid.NewGuid();
            await _employee.Employees.AddAsync(employee);
            await _employee.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetEmployeeById([FromBody] Guid id)
        {
            var employee = await _employee.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
    }
}

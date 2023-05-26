using Employee.Data;
using Employee.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDetails.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext dbContext;

        public EmployeeController(EmployeeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeDetails()
        {
            return Ok(await dbContext.Employees.ToListAsync());

        }

        [HttpGet]
        [Route("employeById/{id:long}")]
        public async Task<IActionResult> GetEmployeById([FromRoute] long id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]

        public async Task<IActionResult> AddEmployee(AddEmployeeRequest addEmployeeRequest)
        {

            var employee = new EmployeeDto()
            {
               
                Name = addEmployeeRequest.Name,
                Qualification = addEmployeeRequest.Qualification,
                Position = addEmployeeRequest.Position,
                Email = addEmployeeRequest.Email

            };
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return Ok("Employee Record Added successfully");

        }
        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] long id, UpdateEmployeeRequest updateEmployeeRequest)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                employee.Name = updateEmployeeRequest.Name;
                employee.Qualification = updateEmployeeRequest.Qualification;
                employee.Position = updateEmployeeRequest.Position;
                employee.Email = updateEmployeeRequest.Email;

                await dbContext.SaveChangesAsync();
                return Ok("Employee Record Updated successfully");

            }
            return NotFound();

        }

        [HttpDelete]
        [Route("delete/{id}")]

        public async Task<IActionResult> DeleteEmployee([FromRoute] long id)
        {
            var employee = await dbContext.Employees.FindAsync(id);
            if (employee != null)
            {
                dbContext.Remove(employee);
                await dbContext.SaveChangesAsync();
                return Ok("Employee Record deleted successfully");
            }
            return NotFound();
        }
    }
}

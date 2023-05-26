using Employee.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
            {

            }

        public DbSet<EmployeeDto> Employees{ get; set; }
    }
}

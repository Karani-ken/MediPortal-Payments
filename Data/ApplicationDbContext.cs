using MediPortal_Payments.Models;
using Microsoft.EntityFrameworkCore;

namespace MediPortal_Payments.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }

        public DbSet<Payment> Payments { get; set; }
    }
}

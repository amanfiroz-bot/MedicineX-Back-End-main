using MedicineX.DbModel;
using Microsoft.EntityFrameworkCore;

namespace MedicineX.DBContext
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
    }
}

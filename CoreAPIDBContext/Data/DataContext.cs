using Microsoft.EntityFrameworkCore;

namespace CoreAPIDBContext.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<UserDTO> Users { get; set; }  
    }
}

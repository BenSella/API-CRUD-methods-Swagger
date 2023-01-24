using Microsoft.EntityFrameworkCore;

namespace Api_Training_Swagger
{
    public class DetailsDb : DbContext
    {
        public DetailsDb(DbContextOptions<DetailsDb> options) : base(options) { }
        public DbSet<Details> Detailss => Set<Details>();
    }
}

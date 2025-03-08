using Microsoft.EntityFrameworkCore;
using RegisterBoook.Api.Models;

namespace RegisterBoook.Api.DataAccess.AppDbContext
{
    public class AppDbContextApi : DbContext
    {
        public AppDbContextApi(DbContextOptions<AppDbContextApi> options) : base(options) { }

        public DbSet<Book> books { get; set; }
        public DbSet<Author> author { get; set; }


    }
}

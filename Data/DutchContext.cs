using Entities.Models;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
  /// <summary>
  /// Derive from Identity entity framework core namespace.
  /// add mention what the type of the identity user is.
  /// After switching to IdentityDbContext, we have to go open a command prompt
  /// and run the following:
  /// dotnet ef migrations add Identity
  /// That should add the migration to create Identity related tables in the database.
  /// 
  /// </summary>
  public class DutchContext : DbContext
    {
    public DutchContext(DbContextOptions<DutchContext> options): base(options)
    {

    }
        public DbSet<User> Users { get; set; }

        public DbSet<Company> Companies { get; set; }

    }
}

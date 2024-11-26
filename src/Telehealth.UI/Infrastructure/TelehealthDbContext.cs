using Microsoft.EntityFrameworkCore;

namespace Telehealth.UI.Infrastructure;

public class TelehealthDbContext(DbContextOptions<TelehealthDbContext> options) : DbContext(options)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Program.ProgramAssembly);
	}
}

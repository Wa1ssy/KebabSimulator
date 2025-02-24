using Kebab_Simulator.Core.Domain;
using Kebab_Simulator.Core.Domain.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kebab_Simulator.Data
{
	public class KebabSimulatorContext : IdentityDbContext<ApplicationUser>
	{
		public KebabSimulatorContext(DbContextOptions<KebabSimulatorContext> options) : base(options) { }

		public DbSet<FileToDatabase> FilesToDatabase { get; set; }
		public DbSet<Kebab> Kebabs { get; set; }
		public DbSet<PlayerProfile> PlayerProfiles { get; set; }
		public DbSet<KebabOwnership> KebabOwnerships { get; set; }
	}
}

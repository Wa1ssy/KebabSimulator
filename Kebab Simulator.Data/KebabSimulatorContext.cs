using Kebab_Simulator.Core.Domain;
using Kebab_Simulator.Core.Domain.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kebab_Simulator.Data
{
    public class KebabSimulatorContext : DbContext
    {
        public KebabSimulatorContext(DbContextOptions<KebabSimulatorContext> options) : base(options) { }
        public DbSet<FileToDatabase> FilesToDatabase { get; set; }
        
        public DbSet<Kebab> Kebabs { get; set; }
		public DbSet<Galaxy> Galaxies { get; set; }
		public DbSet<IdentityRole> IdentityRoles { get; set; }
		public DbSet<PlayerProfile> PlayerProfiles { get; set; }
		public DbSet<AstralBody> AstralBodies { get; set; }
		public DbSet<SolarSystem> SolarSystems { get; set; }
	}
}

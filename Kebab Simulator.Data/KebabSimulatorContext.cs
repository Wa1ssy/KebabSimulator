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
    public class KebabSimulatorContext : DbContext<ApplicationUser>
    {
        public KebabSimulatorContext(DbContextOptions<KebabSimulatorContext> options) : base(options) { }
        public DbSet<FileToDatabase> FilesToDatabase { get; set; }
        
        public DbSet<Kebab> Kebabs { get; set; }
		public DbSet<Country> Countries { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<FileToDatabase> FileToDatabases { get; set; }
		public DbSet<IdentityRole> IdentityRoles { get; set; }
		public DbSet<PlayerProfile> PlayerProfiles { get; set; }
        public DbSet<KebabOwnership> KebabOwnerships { get; set;}
	}
}

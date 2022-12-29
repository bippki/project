using Microsoft.EntityFrameworkCore;
using projectus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectus.ORMDAL
{
	public class DefaultDBContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<GameRecord> GameRecords { get; set; }

		public DefaultDBContext() 
		{
			Database.EnsureCreated();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DefaultDBContext;Trusted_Connection=True;MultipleActiveResultSets=true");
		}
	}
}

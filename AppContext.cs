using Microsoft.EntityFrameworkCore;
using MematorSQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MematorSQL
{
	public class AppContext : DbContext
	{
		public DbSet<Meme> Memes { get; set; }
		public DbSet<Meme> RandomMemes { get; set; }

		public AppContext()
		{
			Logger.Debug("Создание AppContext");
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			Logger.Debug("Вызван AppContext.OnConfiguring");
			optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=memedb;Username=postgres;Password=25565");
		}
	}
}

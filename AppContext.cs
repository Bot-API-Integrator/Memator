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
		private const string _connectionString = "Host=localhost;Port=5432;Database=memedb;Username=postgres;Password=25565";
		public DbSet<Meme> Memes { get; set; }
		public DbSet<RandomMeme> RandomMemes { get; set; }

		public AppContext()
		{
			Logger.Debug("Создание AppContext");
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			Logger.Debug("Вызван AppContext.OnConfiguring");
			optionsBuilder.UseNpgsql(_connectionString);
		}
	}
}

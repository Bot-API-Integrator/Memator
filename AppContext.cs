using Microsoft.EntityFrameworkCore;
using MematorSQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MematorSQL
{
	/// <summary>
	/// docker run -e POSTGRES_PASSWORD=25565 -e POSTGRES_USER=postgres -e POSTGRES_DB=memedb -p 5432:5432 -d postgres
	/// </summary>
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
			optionsBuilder
				.UseLazyLoadingProxies()
				.UseNpgsql(_connectionString);
		}
	}
}

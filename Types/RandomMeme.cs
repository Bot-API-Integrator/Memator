using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MematorSQL.Types
{
	public class RandomMeme
	{
		public int Id { get; set; }
		public String Name { get; set; }
		public virtual RandomImage Image { get; set; }

		public RandomMeme() { }
		public RandomMeme(String name, RandomImage image)
		{
			Name = name;
			Image = image;
		}
	}
}

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
		public Image Image { get; set; }

		public RandomMeme() { }
		public RandomMeme(String name, Image image)
		{
			Name = name;
			Image = image;
		}
	}
}

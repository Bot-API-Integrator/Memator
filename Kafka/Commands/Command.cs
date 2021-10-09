using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MematorSQL.Kafka.Commands
{
	public class Command
	{
		public String Name;
		private Action action;
	}
}

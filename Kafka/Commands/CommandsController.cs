using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MematorSQL.Kafka.Commands
{
	public static class CommandsController
	{
		private static List<Command> commands = new List<Command>();

		public static void AddCommand(Command command)
		{
			if (commands.Contains(command))
				return;

			if (commands.Find(c => c.Name == command.Name) != null)
				return;

			commands.Add(command);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MematorSQL.Util;

namespace MematorSQL.Kafka
{
	public class SendMessage
	{
		public String text { get; set; }
		public List<Base64File> photos { get; set; }
	}
}

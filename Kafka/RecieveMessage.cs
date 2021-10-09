using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MematorSQL.Util;

namespace MematorSQL.Kafka { 
	public class RecieveMessage
	{
		public String text { get; set; }
		public List<Base64File> photos { get; set; }
		public String authorId { get; set; }
		public String sendTime { get; set; }
		public String integratorName { get; set; }

	}
}

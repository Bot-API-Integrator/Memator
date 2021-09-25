using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MematorSQL.Types
{
    public class Image
    {
        public string Base64 { get; set; }
        public string FileName { get; set; }

        public Image() { }
        public Image(String path)
        {
			try
			{
				byte[] AsBytes = File.ReadAllBytes(path);
				String AsBase64String = Convert.ToBase64String(AsBytes);
				FileInfo fileInfo = new FileInfo(path);

				Base64 = AsBase64String;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
    }
}

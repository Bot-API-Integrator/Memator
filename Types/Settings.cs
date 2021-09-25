using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MematorSQL.Types
{
    public class Settings
    {
        public string SettingsName { get; private set; } = "DEFAULT";
        public bool DEBUG { get; set; } = true;
        public bool CLEAR_DB_ON_START { get; set; } = true;

        public Settings() { }
        public Settings(String name)
        {
            SettingsName = name;
        }
    }
}

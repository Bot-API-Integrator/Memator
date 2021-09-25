using MematorSQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MematorSQL.Logic
{
    public static class SettingsProvider
    {
        public static Settings DEBUG { get; private set; } = new Settings("DEBUG") 
        {
            DEBUG = true,
            CLEAR_DB_ON_START = true
        };
        public static Settings DEFAULT { get; private set; } = new Settings()
        {
            DEBUG = false,
            CLEAR_DB_ON_START = false
        };

        public static Settings CURRENT { get; private set; } = DEBUG;


    }
}

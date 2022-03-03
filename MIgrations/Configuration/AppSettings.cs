using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations.Configuration
{
    class AppSettings : IAppSettings
    {
        public string ConnectionString { get; set; }
    }
}

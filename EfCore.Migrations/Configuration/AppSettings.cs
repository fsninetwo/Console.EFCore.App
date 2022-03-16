using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCore.Migrations.Configuration
{
    public class AppSettings : IAppSettings
    {
        public string ConnectionString { get; set; }
    }
}

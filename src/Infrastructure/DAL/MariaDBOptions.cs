using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL
{
    internal sealed class MariaDBOptions
    {
        public string ConnectionStringForApplicationDb { get; set; } = string.Empty;
        public string ConnectionStringForIdentity { get; init; } = string.Empty;
        public int VersionMajor { get; set; }
        public int VersionMinor { get; set; }
        public int VersionBuild { get; set; }
    }
}

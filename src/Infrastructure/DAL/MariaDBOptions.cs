using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL
{
    internal sealed class MariaDBOptions
    {
        public string ConnectionStringForIdentity { get; init; } = string.Empty;
    }
}

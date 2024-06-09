using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeCmp.Domain.ValueObjects;

namespace TreeCmp.Domain.Entities
{
    public sealed class PhylogenyTree(PhylogenyTreeId id, PhylogenyTreeName name, PhylogenyTreeBody body)
    {
        public PhylogenyTreeId Id { get; set; } = id;
        public PhylogenyTreeName Name { get; set; } = name;
        public PhylogenyTreeBody Body { get; set; } = body;
    }
}

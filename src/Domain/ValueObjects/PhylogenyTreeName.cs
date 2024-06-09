namespace TreeCmp.Domain.ValueObjects
{
    public sealed record PhylogenyTreeName(string Value)
    {
        public static implicit operator string(PhylogenyTreeName name) => name.Value;
        public static implicit operator PhylogenyTreeName(string name) => new(name);
    }
}
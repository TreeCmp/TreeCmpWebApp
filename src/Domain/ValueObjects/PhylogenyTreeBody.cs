namespace TreeCmp.Domain.ValueObjects
{
    public sealed record PhylogenyTreeBody(string Value)
    {
        public static implicit operator string(PhylogenyTreeBody name) => name.Value;
        public static implicit operator PhylogenyTreeBody(string name) => new(name);
    }
}
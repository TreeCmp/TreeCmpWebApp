namespace TreeCmp.Domain.ValueObjects
{
    public sealed record PhylogenyTreeId(int Value)
    {
        public static implicit operator int(PhylogenyTreeId id) => id.Value;
        public static implicit operator PhylogenyTreeId(int id) => new(id);
    }
}
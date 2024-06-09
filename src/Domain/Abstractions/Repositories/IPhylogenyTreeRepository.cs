using TreeCmp.Domain.Entities;
using TreeCmp.Domain.ValueObjects;

namespace Domain.Abstractions.Repositories
{
    public interface IPhylogenyTreeRepository
    {
        Task<PhylogenyTree?> GetAsync<T>(PhylogenyTreeId id, CancellationToken ct);
        Task AddAsync(PhylogenyTree tree, CancellationToken ct);
        Task UpdateAsync(PhylogenyTree tree, CancellationToken ct);
        Task DeleteAsync(PhylogenyTree tree, CancellationToken ct);
    }
}

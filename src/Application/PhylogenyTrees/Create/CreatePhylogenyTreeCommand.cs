using MediatR;
using TreeCmp.Domain.ValueObjects;

namespace Application.PhylogenyTrees.Create
{
    public sealed record CreatePhylogenyTreeCommand(PhylogenyTreeId id, PhylogenyTreeName name, PhylogenyTreeBody body) : IRequest;
}

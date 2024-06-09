using Domain.Abstractions.Repositories;
using MediatR;
using TreeCmp.Domain.Entities;

namespace Application.PhylogenyTrees.Create
{
    internal sealed class CreatePhylogenyTreeCommandHandler(IPhylogenyTreeRepository phylogenyTreeRepository)
    : IRequestHandler<CreatePhylogenyTreeCommand>
    {
        public Task Handle(CreatePhylogenyTreeCommand request, CancellationToken ct)
        {
            var newPhylogenyTree = new PhylogenyTree(request.id, request.name, request.body);
            return phylogenyTreeRepository.AddAsync(newPhylogenyTree, ct);
        }
    }
}

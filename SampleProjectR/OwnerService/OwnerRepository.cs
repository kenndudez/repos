using SampleProjectR.Context;
using SampleProjectR.Models;
using SampleProjectR.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProjectR.OwnerService
{
    public class OwnerRepository : RepositoryBase<Owners>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) { }
    }
}

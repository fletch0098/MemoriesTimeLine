using System;
using System.Collections.Generic;
using System.Text;
using MTL.DataAccess.Contracts;
using MTL.DataAccess.Entities;

namespace MTL.DataAccess.Repository
{
    public class MemoryRepository : RepositoryBase<Memory>, IMemoryRepository
    {
        public MemoryRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}

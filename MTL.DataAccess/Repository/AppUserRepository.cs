using System;
using System.Collections.Generic;
using System.Text;
using MTL.DataAccess.Contracts;
using MTL.DataAccess.Entities;

namespace MTL.DataAccess.Repository
{
    public class AppUserRepository : RepositoryBase<AppUser>, IAppUserRepository
    {
        public AppUserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}

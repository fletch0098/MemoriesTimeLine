using System;
using System.Collections.Generic;
using System.Text;
using MTL.DataAccess.Entities;
using MTL.DataAccess.Repository;
using System.Linq;
using System.Linq.Expressions;

namespace MTL.DataAccess.Contracts
{
    public interface IAppUserRepository
    {
        IEnumerable<AppUser> FindAll();
        IEnumerable<AppUser> FindByCondition(Expression<Func<AppUser, bool>> expression);
        //AppUser FindByName(string userName);
        void Create(AppUser entity, string password);
        void Update(AppUser entity);
        void Delete(AppUser entity);
        void Save();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using MTL.DataAccess.Contracts;
using Microsoft.AspNetCore.Identity;
using MTL.DataAccess.Entities;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

// var localUser = await _userManager.FindByNameAsync(userInfo.Email);

namespace MTL.DataAccess.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        protected RepositoryContext RepositoryContext { get; set; }
        private readonly UserManager<AppUser> _userManager;

        public AppUserRepository(RepositoryContext repositoryContext, UserManager<AppUser> userManager)
        {
            this.RepositoryContext = repositoryContext;
            this._userManager = userManager;
        }

        public IEnumerable<AppUser> FindAll()
        {
            List<AppUser> appUsers = new List<AppUser>();
            return appUsers;
        }

        public IEnumerable<AppUser> FindByCondition(Expression<Func<AppUser, bool>> expression)
        {
            return this.RepositoryContext.Set<AppUser>().Where(expression);
        }

        public void Create(AppUser entity, string password)
        {
            var result = _userManager.CreateAsync(entity, password);
        }

        //public AppUser FindByNameAsync(string userName)
        //{
        //    var user = _userManager.FindByNameAsync(userName);
        //    return user;
        //}

        public void Update(AppUser entity)
        {
            this.RepositoryContext.Set<AppUser>().Update(entity);
        }

        public void Delete(AppUser entity)
        {
            this.RepositoryContext.Set<AppUser>().Remove(entity);
        }

        public void Save()
        {
            this.RepositoryContext.SaveChanges();
        }
    }
}

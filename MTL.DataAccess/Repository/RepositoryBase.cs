using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using MTL.DataAccess.Contracts;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace MTL.DataAccess.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; }
        private readonly ILogger<RepositoryWrapper> _logger;

        #region SYNC
        public RepositoryBase(RepositoryContext repositoryContext, ILogger<RepositoryWrapper> logger)
        {
            this.RepositoryContext = repositoryContext;
            this._logger = logger;
        }

        public IEnumerable<T> FindAll()
        {
            IEnumerable<T> ret = null;

            Type typeParameterType = typeof(T);

            try
            {
                var query = this.RepositoryContext.Set<T>();

                if (query != null)
                {
                    ret = query.ToList();
                    _logger.LogInformation(string.Format("{0} : {1} {2} found", System.Reflection.MethodBase.GetCurrentMethod(), ret.Count(), typeParameterType.Name));
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} : No {1} were found", System.Reflection.MethodBase.GetCurrentMethod(), typeParameterType.Name));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while looking for {1}", System.Reflection.MethodBase.GetCurrentMethod(), typeParameterType.Name));
            }

            return ret;
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            IEnumerable<T> ret = null;

            Type typeParameterType = typeof(T);

            try
            {
                var query = this.RepositoryContext.Set<T>().Where(expression);

                if (query != null)
                {
                    ret = query.ToList();
                    _logger.LogInformation(string.Format("{0} : {1} {2} found", System.Reflection.MethodBase.GetCurrentMethod(), ret.Count(), typeParameterType.Name));
                }
                else
                {
                    _logger.LogWarning(string.Format("{0} : No {1} were found", System.Reflection.MethodBase.GetCurrentMethod(), typeParameterType.Name));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while looking for {1}", System.Reflection.MethodBase.GetCurrentMethod(), typeParameterType.Name));
            }

            return ret;
        }

        public void Create(T entity)
        {
            Type typeParameterType = typeof(T);

            try
            {
                var x = this.RepositoryContext.Set<T>().Add(entity);

                //if (x. > 0)
                //{
                //    ret = Memory.id;

                    _logger.LogInformation(string.Format("{0} : Added {1}", System.Reflection.MethodBase.GetCurrentMethod(), typeParameterType.Name));
                //}
                //else
                //{
                    //_logger.LogWarning(string.Format("{0} : No Memories were added", System.Reflection.MethodBase.GetCurrentMethod()));
                //}
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Format("{0} : An error occured while adding to {1}", System.Reflection.MethodBase.GetCurrentMethod(), typeParameterType.Name));
            }
            
        }

        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Remove(entity);
        }

        public void Save()
        {
            this.RepositoryContext.SaveChanges();
        }
        #endregion

        #region ASYNC
        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await this.RepositoryContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAync(Expression<Func<T, bool>> expression)
        {
            return await this.RepositoryContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await this.RepositoryContext.SaveChangesAsync();
        }
        #endregion
    }
}

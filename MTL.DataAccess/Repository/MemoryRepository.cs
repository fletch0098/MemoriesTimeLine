using System.Collections.Generic;
using MTL.DataAccess.Contracts;
using MTL.DataAccess.Entities;
using MTL.DataAccess.Entities.Extensions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace MTL.DataAccess.Repository
{
    public class MemoryRepository : RepositoryBase<Memory>, IMemoryRepository
    {
        private readonly ILogger<RepositoryWrapper> _logger;

        public MemoryRepository(RepositoryContext repositoryContext, ILogger<RepositoryWrapper> logger)
            : base(repositoryContext, logger)
        {

        }

        #region SYNC
        public IEnumerable<Memory> GetAllMemories()
        {
            return FindAll()
                .OrderBy(tl => tl.Name);
        }

        public Memory GetMemoryById(int id)
        {
            return FindByCondition(m => m.Id.Equals(id))
                .DefaultIfEmpty(new Memory())
                .FirstOrDefault();
        }

        public Memory GetMemoryByTimeLineId(int timeLineId)
        {
            return FindByCondition(m => m.TimeLineId.Equals(timeLineId))
                .DefaultIfEmpty(new Memory())
                .FirstOrDefault();
        }

        public MemoryExtended GetMemoryWithDetails(int id)
        {
            return new MemoryExtended(GetMemoryById(id))
            {
                TimeLine = RepositoryContext.TimeLines
                    .Where(x => x.Id == id).FirstOrDefault()
            };
        }

        public int CreateMemory(Memory memory)
        {
            Create(memory);
            Save();
            return memory.Id;
        }

        public void UpdateMemory(int id, Memory memory)
        {
            Memory dbMemory = GetMemoryById(id);
            memory.Modified();
            dbMemory.Map(memory);
            Update(dbMemory);
            Save();
        }

        public void DeleteMemory(int id)
        {
            Memory dbMemory = GetMemoryById(id);
            Delete(dbMemory);
            Save();
        }
        #endregion

        #region ASYNC
        public async Task<IEnumerable<Memory>> GetAllMemorysAsync()
        {
            var memorys = await FindAllAsync();
            return memorys.OrderBy(x => x.Name);
        }

        public async Task<Memory> GetMemoryByIdAsync(int id)
        {
            var memory = await FindByConditionAync(o => o.Id.Equals(id));
            return memory.DefaultIfEmpty(new Memory())
                    .FirstOrDefault();
        }

        public async Task<MemoryExtended> GetMemoryWithDetailsAsync(int id)
        {
            var memory = await GetMemoryByIdAsync(id);

            return new MemoryExtended(memory)
            {
                //TimeLine = await RepositoryContext.TimeLines
                //    .Where(a => a. == id).ToListAsync()
            };
        }

        public async Task<int> CreateMemoryAsync(Memory memory)
        {
            Create(memory);
            await SaveAsync();
            return memory.Id;
        }

        public async Task UpdateMemoryAsync(int id, Memory memory)
        {
            Memory dbMemory = await GetMemoryByIdAsync(id);
            dbMemory.Modified();
            dbMemory.Map(memory);
            Update(dbMemory);
            await SaveAsync();
        }

        public async Task DeleteMemoryAsync(int id)
        {
            Memory dbMemory = await GetMemoryByIdAsync(id);
            Delete(dbMemory);
            await SaveAsync();
        }
        #endregion
    }
}

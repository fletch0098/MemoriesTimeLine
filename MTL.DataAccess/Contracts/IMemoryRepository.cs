using System.Collections.Generic;
using MTL.DataAccess.Entities;
using System.Threading.Tasks;

namespace MTL.DataAccess.Contracts
{
    public interface IMemoryRepository : IRepositoryBase<Memory>
    {
        #region SYNC
        IEnumerable<Memory> GetAllMemories();
        Memory GetMemoryById(int id);
        IEnumerable<Memory> GetMemoriesByTimeLineId(int timeLineId);
        MemoryExtended GetMemoryWithDetails(int id);
        int CreateMemory(Memory memory);
        void UpdateMemory(int id, Memory memory);
        void DeleteMemory(int id);
        #endregion

        #region ASYNC
        Task<IEnumerable<Memory>> GetAllMemoriesAsync();
        Task<Memory> GetMemoryByIdAsync(int id);
        Task<IEnumerable<Memory>> GetMemoriesByTimeLineIdAsync(int timeLineId);
        Task<MemoryExtended> GetMemoryWithDetailsAsync(int Id);
        Task<int> CreateMemoryAsync(Memory memory);
        Task UpdateMemoryAsync(int id, Memory memory);
        Task DeleteMemoryAsync(int id);
        #endregion
    }
}

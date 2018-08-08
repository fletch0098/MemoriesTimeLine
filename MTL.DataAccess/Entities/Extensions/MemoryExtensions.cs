using System;
using System.Collections.Generic;
using System.Text;

namespace MTL.DataAccess.Entities.Extensions
{
    public static class MEmoryExtensions
    {
        public static void Map(this Memory dbMemory, Memory memory)
        {
            dbMemory.Name = memory.Name;
            dbMemory.Description = memory.Description;
            dbMemory.LastModified = memory.LastModified;
            dbMemory.Date = memory.Date;
            dbMemory.TimeLineId = memory.TimeLineId;
            dbMemory.Id = memory.Id;
        }
    }
}

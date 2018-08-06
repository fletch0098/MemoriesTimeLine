using System;
using System.Collections.Generic;
using System.Text;

namespace MTL.DataAccess.Contracts
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime LastModified { get; set; }

        void Modified();
    }
}

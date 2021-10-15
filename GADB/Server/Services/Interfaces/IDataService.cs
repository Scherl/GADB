using GADB.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GADB.Server.Interfaces
{
    public interface IDataService
    {
        /// <summary>
        /// Get HQ entity if entiy is marked as HQ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Data> GetHQ(Guid id);
    }
}

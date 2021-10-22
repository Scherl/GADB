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
        /// Get all institutions marked as HQ
        /// </summary>
        /// <returns>List of all HQ institutions</returns>
        Task<IList<Data>> GetAll();

        /// <summary>
        /// Get institution details by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>object</returns>
        Task<Data> GetById(Guid id);

        /// <summary>
        /// Get HQ entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>object</returns>
        Task<Data> GetHQ(Guid id);

        /// <summary>
        /// Get subsidiaries belonging to one HQ
        /// </summary>
        /// <param name="id"></param>
        /// <returns>list of subsidiaries</returns>
        Task<IList<Data>> GetSubsidiaries(Guid id);






    }
}

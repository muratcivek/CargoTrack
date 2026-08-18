using CargoTrack.DataAccess.Context;
using CargoTrack.DataAccess.Repositories.GenericRepositories;
using CargoTrack.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoTrack.DataAccess.Repositories.Abouts
{
    public class AboutRepository : GenericRepository<About>, IAboutRepository
    {
        public AboutRepository(AppDbContext context) : base(context)
        {
        }

        public Task CreateAsync(About entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(About entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<About>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<About?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(About entity)
        {
            throw new NotImplementedException();
        }
    }
}

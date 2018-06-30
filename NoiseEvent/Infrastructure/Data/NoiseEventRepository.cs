using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;


namespace Infrastructure.Data
{


    public class NoiseEventRepository : EfRepository<NoiseEventEntity>, INoiseEventRepository
    {
        public NoiseEventRepository(NoiseEventContext dbContext) : base(dbContext)
        {
        }

        public NoiseEventEntity GetByDisplayId(Guid id)
        {
            // TODO use Guid
            return _dbContext.NoiseEvent.FirstOrDefault();
        }

        //public async Task<NoiseEventEntity> GetByDisplayIdAsync(Guid id)
        //{
        //    // TODO use Guid
        //    var result = _dbContext.NoiseEvent.FirstOrDefault();   //.FirstOrDefaultAsync();    FAKE NOT ASYNC
        //    return result;
        //}



        //Task<DisplayEntity> GetByIdAsync(Guid id);
        //DisplayEntity GetById(Guid id);



        //public DisplayEntity GetById(int id)
        //{
        //    return _dbContext.Displays.FirstOrDefault();
        //}

        //public Task<DisplayEntity> GetByIdWithItemsAsync(int id)
        //{
        //    return _dbContext.Displays.FirstOrDefaultAsync();
        //}
    }
}

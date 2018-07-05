using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace Infrastructure.Data
{


    public class NoiseEventRepository : EfRepository<NoiseEventEntity>, INoiseEventRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<NoiseEventRepository> _logger;


        public NoiseEventRepository(
           NoiseEventContext dbContext,            // base class stores this
           ILogger<NoiseEventRepository> logger,
           IConfiguration configuration)
           : base(dbContext)
        {
        }

        public NoiseEventEntity GetByNoiseEventId(Guid id)
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

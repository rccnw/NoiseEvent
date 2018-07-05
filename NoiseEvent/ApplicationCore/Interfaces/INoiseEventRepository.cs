using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{


    public interface INoiseEventRepository : IRepository<NoiseEventEntity>, IAsyncRepository<NoiseEventEntity>
    {
        //Task<DisplayEntity> GetByIdAsync(Guid id);
        //DisplayEntity GetById(Guid id);

        //Task<NoiseEventEntity> GetByDisplayIdAsync(Guid id);
        NoiseEventEntity GetByNoiseEventId(Guid id);
    }
}

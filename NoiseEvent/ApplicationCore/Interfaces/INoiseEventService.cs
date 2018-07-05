using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTO;

namespace ApplicationCore.Interfaces
{
    public interface INoiseEventService
    {
        Task<List<NoiseEvent>> GetAllNoiseEventsAsync();
    }
}

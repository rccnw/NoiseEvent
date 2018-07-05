using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.DTO;
using ApplicationCore.Interfaces;
using AutoMapper;
using System.Diagnostics;
using ApplicationCore.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ApplicationCore.Services
{
    public class NoiseEventService : INoiseEventService
    {

        private readonly INoiseEventRepository _noiseEventRepository;

        private readonly ILogger<NoiseEventService> _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public NoiseEventService(
            INoiseEventRepository noiseEventRepository,
            ILogger<NoiseEventService> logger,
            IConfiguration configuration,
            IMapper mapper)
        {
            _noiseEventRepository = noiseEventRepository;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }


        #region API
        public async Task<List<NoiseEvent>> GetAllNoiseEventsAsync()
        {
            var listDto = new List<NoiseEvent>();
            try
            {
                var listEntity = await _noiseEventRepository.ListAllAsync();
                foreach (var entity in listEntity)
                {
                    listDto.Add(CreateNoiseEventDTO(entity));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                _logger.LogError(ex, "NoiseEventService.GetAllNoiseEventsAsync");
            }
            return listDto;
        }

        #endregion


        #region Helper
        private NoiseEvent CreateNoiseEventDTO(NoiseEventEntity entity)
        {
            var dto = new NoiseEvent()
            {
                Description = entity.Description,
                Location = entity.Location
            };
            return dto;
        }
        #endregion



    }
}

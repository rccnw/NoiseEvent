using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Interfaces;
using ApplicationCore.Logging;
using Microsoft.Extensions.Logging;
using ApplicationCore.Exceptions;

namespace NoiseEvent.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class NoiseEventController : ControllerBase
    {

        private readonly INoiseEventService _noiseEventService;
        private readonly ILogger<NoiseEventController> _logger;
        private readonly IApplicationInsightsLogger _applicationInsightsLogger;

        public NoiseEventController(
            INoiseEventService noiseEventService,       // application service : provides access to business rules and data repository
            ILogger<NoiseEventController> logger,
            IApplicationInsightsLogger applicationInsightsLogger)
        {
            _noiseEventService = noiseEventService;
            _logger = logger;
            _applicationInsightsLogger = applicationInsightsLogger;
        }


        // [HttpGet("{id}", Name = "NoiseData")]

        //public async Task<IActionResult> GetAllAsync(Guid id)
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var noiseData = await _noiseEventService.GetAllNoiseEventsAsync(); // application service layer 
                return Ok(noiseData);
            }
            catch (EventNotFoundException ex)
            {
                //var properties = new Dictionary<string, string>
                //{
                //   {"Display Id: ", id.ToString()}
                //};
                //_applicationInsightsLogger.TrackException(ex, properties);
               // _logger.LogError($"NoiseEventController.GetByIdAsync - GetByIdAsync failed - id:  {id}");
                return NotFound();
            }
        }


#if NADA
        private INoiseEventRepository _noiseEventRepository;
        private ILogger<NoiseEventController> _logger;

        public NoiseEventController(ILogger<NoiseEventController> logger)
        {
            _logger = logger;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] NoiseEventForCreationDto noiseEvent)
        {
            if (noiseEvent == null)
            {
                return BadRequest();
            }
            if (noiseEvent.Description == noiseEvent.Location)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the Location.");
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _noiseEventRepository.AddNoiseEvent(noiseEvent);

            if (!_noiseEventRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }



            return Ok();

        }


       // [HttpPost ("noiseevent")]
        //public IActionResult CreateNoiseEvent([FromBody] NoiseEventDto noiseEvent)
        //{
        //    if (noiseEvent==null)
        //    {
        //        return BadRequest();
        //    }

        //    if (noiseEvent.Description == noiseEvent.Location)
        //    {
        //        ModelState.AddModelError("Description", "The provided description should be different from the Location.");
        //    }


        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }




        //    _noiseEventRepository.AddNoiseEvent(noiseEvent);

        //    if (!_noiseEventRepository.Save())
        //    {
        //        return StatusCode(500, "A problem happened while handling your request.");
        //    }



        //    return Ok();
        //}

        public void CreateNoiseEvent(NoiseEventDto noiseEvent)
        {
        }

        // GET api/values
        [HttpGet]
        public JsonResult Get()
        {
            var ld = LogDetail.GetLogDetail("Get", null);
            Logger.WriteUsage(ld);
            //return new string[] { "event1", "event2" };
            return new JsonResult(NoiseEventsDataStore.Current.NoiseEvents);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }



        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

#endif

    }
}
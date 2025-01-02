using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pabio.Filters;
using pabio.Models.Events;
using pabio.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pabio.Controllers
{
    [Route("api/events")]
    [ApiController]
    //[Authorize]
    public class EventApiController : ControllerBase
    {
        private readonly EventService _service;
        private readonly ILogger<EventApiController> _logger;
        public EventApiController(EventService service,
            ILogger<EventApiController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/events
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                var events = await _service.GetEvents();
                _logger.LogInformation($"API invoked to get all events. Returned {0}", events.Count);
                return Ok(events);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Get events list error");
                return BadRequest();
            }
        }

        // GET api/events/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        //[EnsureEventExists]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var eventItem = await _service.GetEvent(id);
                _logger.LogInformation($"API invoked to get event by id={0}", id);
                return Ok(eventItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Get event by id={id} error");
                return BadRequest();
            }
        }

        // POST api/events
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEventCommand input)
        {
            try
            {
                if (input != null)
                {
                    var id = await _service.CreateEvent(input);
                    _logger.LogInformation($"New event(id={id}) created by API invoke");
                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Event creation error");
                return BadRequest();
            }
            return BadRequest();
        }

        // PUT api/events/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateEventCommand input)
        {
            try
            {
                if (input != null)
                {
                    await _service.UpdateEvent(input);
                    _logger.LogInformation($"Event(id={id}) updated by API invoke");
                    return Ok(id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Event update error");
                return BadRequest();
            }
            return BadRequest();
        }

        // DELETE api/events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteEvent(id);
                _logger.LogInformation($"Event(id={id}) deleted by API invoke");
                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Event deletion error");
                return BadRequest();
            }
        }
    }
}

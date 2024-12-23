using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pabio.Filters;
using pabio.Models.Events;
using pabio.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pabio.Controllers
{
    [Route("api/events")]
    [ApiController]
    [Authorize]
    public class EventApiController : ControllerBase
    {
        private readonly EventService _service;
        public EventApiController(EventService service)
        {
            _service = service;
        }

        // GET: api/events
        [HttpGet]
        [AllowAnonymous]
        public Task<List<Event>> Get()
        {
            return _service.GetEvents();
            //return new string[] { "value1", "value2" };
        }

        // GET api/events/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        //[EnsureEventExists]
        public async Task<IActionResult> Get(int id)
        {
            var eventItem = await _service.GetEvent(id);
            return Ok(eventItem);
            //if (eventItem == null)
            //    return NotFound();


        }

        // POST api/events
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/events/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/events/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

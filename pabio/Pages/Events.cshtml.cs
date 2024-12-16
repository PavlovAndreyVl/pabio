using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pabio.Data;
using pabio.Services;

namespace pabio.Pages
{
    public class EventsModel : PageModel
    {
        private readonly EventService _service;

        public EventsModel(EventService service)
        {
            _service = service;
        }

        public IList<Event> Events { get; private set; } = default!;

        public async Task OnGetAsync()
        {
            Events = await _service.GetEvents();
        }
    }
}

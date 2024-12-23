using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pabio.Models.Events;
using pabio.Services;

namespace pabio.Pages.Events
{
    public class EditModel(EventService service,
        IAuthorizationService authorizationService,
        ILogger<IndexModel> logger) : PageModel
    {
        [BindProperty]
        public UpdateEventCommand? Input { get; set; }
        private readonly EventService _service = service;
        public readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly ILogger<IndexModel> _logger = logger;

        public async Task<IActionResult> OnGet(int id)
        {

            Input = await _service.GetEventForUpdate(id);
            if (Input is null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid
                    && Input != null)
                {
                    await _service.UpdateEvent(Input);
                    _logger.LogInformation($"Eevent(id={Input.Id}) updated by {User.Identity?.Name}");
                    return RedirectToPage("List");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update event error");
                ModelState.AddModelError(
                    string.Empty,
                    "An error occured saving the event"
                    );
            }
            return Page();
        }
    }
}

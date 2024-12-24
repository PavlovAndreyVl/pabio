using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pabio.Models.Gamplitune;
using pabio.Services;

namespace pabio.Pages.Gamplitune
{
    public class RequestModel(IAuthorizationService authorizationService,
        ILogger<IndexModel> logger) : PageModel
    {
        [BindProperty]
        public Request? Input { get; set; }

        public readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly ILogger<IndexModel> _logger = logger;

        public void OnGet()
        {
            Input = new Request();
        }

        public Task<IActionResult> OnPost()
        {
            return Task.FromResult<IActionResult>(RedirectToPage("Result", Input));
            //try
            //{
            //    //if (ModelState.IsValid
            //    //    && Input != null)
            //    //{
            //    //    var id = await _service.CreateEvent(Input);
            //    //    _logger.LogInformation($"New event(id={id}) created by {User.Identity?.Name}");
            //    //    return RedirectToPage("List", new { id = id });
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    //_logger.LogError(ex, "Event creation error");
            //    //// Add a model-level error by using an empty string key
            //    //ModelState.AddModelError(
            //    //    string.Empty,
            //    //    "При створенні події виникла помилка"
            //    //);
            //}

            //return Page();
        }
    }
}

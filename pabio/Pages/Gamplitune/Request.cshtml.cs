using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pabio.Models.Gamplitune;
using pabio.Services;

namespace pabio.Pages.Gamplitune
{
    [Authorize]
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

        //public Task<IActionResult> OnPost()
        //{
        //    return Task.FromResult<IActionResult>(RedirectToPage("Result", Input));

        //}
    }
}

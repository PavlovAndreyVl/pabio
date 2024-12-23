using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pabio.Models.Events;
using pabio.Services;

namespace pabio.Pages.Events
{
    public class ListModel : PageModel
    {
        private readonly EventService _service;
        public readonly IAuthorizationService _authorizationService;

        public bool CanAddNew { get; set; }

        public ListModel(EventService service, IAuthorizationService authorizationService)
        {
            _service = service;
            _authorizationService = authorizationService;
        }

        public IList<Event> Events { get; private set; } = default!;

        public async Task OnGetAsync()
        {
            if (User != null && User.Identity != null)
                CanAddNew = User.Identity.IsAuthenticated;

            //AuthorizationResult isAuthorized = await _authService
            //  .AuthorizeAsync(User, recipe, "CanManageRecipe");
            //CanEditRecipe = isAuthorized.Succeeded;
            Events = await _service.GetEvents();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _service.DeleteEvent(id);

            return RedirectToPage("List");
        }
    }
}

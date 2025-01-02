using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace pabio.Pages
{
    //[Authorize(Constants.Policies.GLOBAL_ADMIN)]
    public class FamilyModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}

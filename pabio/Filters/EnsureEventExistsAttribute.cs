using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using pabio.Services;

namespace pabio.Filters
{
    public class EnsureEventExistsAttribute : TypeFilterAttribute
    {
        public EnsureEventExistsAttribute() : base(typeof(EnsureEventExistsAttribute)) { }

        public class EnsureEventExistsFilter : IAsyncActionFilter
        {
            private readonly EventService _service;
            public EnsureEventExistsFilter(EventService service)
            {
                _service = service;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var eventId = (int)context.ActionArguments["id"];
                if (!await _service.DoesEventExistAsync(eventId))
                {
                    context.Result = new NotFoundResult();
                }
                else
                {
                    await next();
                }
            }
        }
    }
}

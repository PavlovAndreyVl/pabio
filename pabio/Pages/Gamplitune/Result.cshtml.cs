using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using pabio.Models.Gamplitune;
using pabio.Services;

namespace pabio.Pages.Gamplitune
{
    public class ResultModel (ChatGptService aiService,
        ILogger<IndexModel> logger) : PageModel
    {
        public Request? Input { get; set; }
        ChatGptService _aiService = aiService;
        private readonly ILogger<IndexModel> _logger = logger;
        public string? AiResponse { get; set; }

        public async Task<IActionResult> OnGetAsync(Request? input)
        {
            Input = input;
            if (input == null)
                return BadRequest();

            try
            {
                string prompt = $"Уяви що ти музикант і вправно граєш на електрогітарі. " +
                    $"Тобі треба налаштувати на гітарному підсилювачі {input.AmpModel} " +
                    $"звук як у пісні {input.Song} виконавця {input.Artist}. Якби ті це зробив якнайкраще?";
                string markdownResponce = await _aiService.GetResponse(prompt);
                AiResponse = Markdown.ToHtml(markdownResponce);
                _logger.LogInformation($"New request to Gamplitune {User.Identity?.Name} successfully executed");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, $"Gamplitune request error for user {User.Identity?.Name}");
                ModelState.AddModelError(
                    string.Empty,
                    "При формуванні відповіді на запит виникла помилка"
                );
            }
            return Page();

        }
    }
}

using Markdig;
using Microsoft.AspNetCore.Mvc;
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

            string prompt = $"”€ви що ти музикант ≥ вправно граЇш на електрог≥тар≥. " +
                $"“об≥ треба налаштувати на г≥тарному п≥дсилювач≥ {input.AmpModel} " +
                $"звук €к у п≥сн≥ {input.Song} виконавц€ {input.Artist}. якби т≥ це зробив €кнайкраще?";
            string markdownResponce = await _aiService.GetResponse(prompt);
            AiResponse = Markdown.ToHtml(markdownResponce);
            return Page();
        }
    }
}

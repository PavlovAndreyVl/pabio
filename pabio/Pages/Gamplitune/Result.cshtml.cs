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

            string prompt = $"���� �� �� �������� � ������� ���� �� �����������. " +
                $"��� ����� ����������� �� �������� ���������� {input.AmpModel} " +
                $"���� �� � ��� {input.Song} ��������� {input.Artist}. ���� � �� ������ ����������?";
            string markdownResponce = await _aiService.GetResponse(prompt);
            AiResponse = Markdown.ToHtml(markdownResponce);
            return Page();
        }
    }
}

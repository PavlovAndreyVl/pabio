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
                string prompt = $"���� �� �� �������� � ������� ���� �� �����������. " +
                    $"��� ����� ����������� �� �������� ���������� {input.AmpModel} " +
                    $"���� �� � ��� {input.Song} ��������� {input.Artist}. ���� � �� ������ ����������?";
                string markdownResponce = await _aiService.GetResponse(prompt);
                AiResponse = Markdown.ToHtml(markdownResponce);
                _logger.LogInformation($"New request to Gamplitune {User.Identity?.Name} successfully executed");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, $"Gamplitune request error for user {User.Identity?.Name}");
                ModelState.AddModelError(
                    string.Empty,
                    "��� ��������� ������ �� ����� ������� �������"
                );
            }
            return Page();

        }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace pabio.Models.Events
{
    public class EditEventBase
    {
        [DisplayName("Посилання")]
        public string? Url { get; set; }

        [Required, DisplayName("Дата")]
        public string Date { get; set; } = string.Empty;

        [Required, DisplayName("Опис")]
        public string Description { get; set; } = string.Empty;

        [Required, DisplayName("Назва")]
        public string Caption { get; set; } = string.Empty;

        public DateTime CreatedAt    { get; set; }
    }
}

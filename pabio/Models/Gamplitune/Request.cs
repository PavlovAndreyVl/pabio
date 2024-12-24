using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace pabio.Models.Gamplitune
{
    public class Request
    {
        [Required,MinLength(3), MaxLength(250), DisplayName("Модель підсилювача")]
        public string? AmpModel { get; set; }

        [Required, MinLength(3), MaxLength(250), DisplayName("Виконавець або група")]
        public string? Artist { get; set; }

        [Required, MinLength(3), MaxLength(250), DisplayName("Назва композиції")]
        public string? Song { get; set; }
    }
}

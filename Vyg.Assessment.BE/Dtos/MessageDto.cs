using System.ComponentModel.DataAnnotations;

namespace Vyg.Assessment.BE.Dtos
{
    public class MessageDto
    {
        [Required]
        public string ProviderName { get; set; } = null!;

        [Required]
        public string ReceipientPhoneNumber { get; set; } = null!;

        [Required]
        public string Message { get; set; } = null!;
    }
}

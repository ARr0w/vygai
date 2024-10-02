using System.ComponentModel.DataAnnotations;

namespace Vyg.Assessment.BE.Dtos
{
    public class UserLoginDto
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}

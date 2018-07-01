using System.ComponentModel.DataAnnotations;

namespace Emailer.Models
{
    public class EmailMessageViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
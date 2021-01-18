using System.ComponentModel.DataAnnotations;

namespace ECAuth.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

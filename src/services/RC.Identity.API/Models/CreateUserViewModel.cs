using System.ComponentModel.DataAnnotations;

namespace RC.Identity.API.Models
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "The field {0} is invalid")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters", MinimumLength = 6)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "The passwords does not match")]
        public string? PasswordConfirmation { get; set; }
    }
}

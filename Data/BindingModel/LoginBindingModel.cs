using System.ComponentModel.DataAnnotations;

namespace RevisendAPI.Data.BindingModel
{
    public class LoginBindingModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace RevisendAPI.Data.BindingModel
{
    public class ChangePasswordBindingModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string NewPassword { get; set; }
    }
}


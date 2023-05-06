using System.ComponentModel.DataAnnotations;

namespace RevisendAPI.Data.BindingModel
{
    public class ForgotPasswordDTO
    {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            public string ClientURI { get; set; }
        
    }
}

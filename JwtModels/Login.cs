using System.ComponentModel.DataAnnotations;

namespace JwtModels
{
   public class Login
   {
      [Required(ErrorMessage = "Email required.")]
      [EmailAddress(ErrorMessage = "Email invalid.")]
      public string Email { get; set; }

      [Required(ErrorMessage = "Password required.")]
      [MinLength(5, ErrorMessage = "Password minimum 5 characters.")]
      public string Password { get; set; }
   }
}

using System.ComponentModel.DataAnnotations;

namespace JwtModels
{
   /// <summary>
   /// Model Login
   /// </summary>
   public class Login
   {
      /// <summary>
      /// E-mail
      /// </summary>
      [Required(ErrorMessage = "Email required.")]
      [EmailAddress(ErrorMessage = "Email invalid.")]
      public string Email { get; set; }

      /// <summary>
      /// Password
      /// </summary>
      [Required(ErrorMessage = "Password required.")]
      [MinLength(5, ErrorMessage = "Password minimum 5 characters.")]
      public string Password { get; set; }
   }
}

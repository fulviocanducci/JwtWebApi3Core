using System;

namespace JwtModels
{
   public class User : Login
   {
      public int Id { get; set; }
      public string Name { get; set; }      
   }
}

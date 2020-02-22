using System;

namespace JwtModels
{
   /// <summary>
   /// Model User
   /// </summary>
   public class User : Login
   {
      /// <summary>
      /// Id
      /// </summary>
      public int Id { get; set; }
      /// <summary>
      /// Name
      /// </summary>
      public string Name { get; set; }      
   }
}

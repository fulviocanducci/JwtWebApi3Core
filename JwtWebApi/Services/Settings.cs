using System.Text;
namespace JwtWebApi.Services
{
   public static class Settings
   {
      public const string Secret = "fedaf7d8863b48e197b9287d492b708e";
      public static byte[] SecretToByte()
      {
         return Encoding.ASCII.GetBytes(Secret);
      }
   }
}

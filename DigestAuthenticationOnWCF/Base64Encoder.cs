using System;
using System.Text;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for encoding a base64 string.
   /// </summary>
   public class Base64Encoder
   {
      public virtual string Encode(string stringToBase64Encode)
      {
         if (stringToBase64Encode != null)
         {
            byte[] encodedStringInBytes = Encoding.ASCII.GetBytes(stringToBase64Encode);
            return Convert.ToBase64String(encodedStringInBytes);
         }
         return null;
      }
   }
}
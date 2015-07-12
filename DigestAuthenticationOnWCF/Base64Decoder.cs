using System;
using System.Text;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for decoding a base64 encoded string.
   /// </summary>
   public class Base64Decoder
   {
      public virtual string Decode(string encodedValue)
      {
         if (encodedValue != null)
         {
            byte[] decodedStringInBytes = Convert.FromBase64String(encodedValue);
            return Encoding.ASCII.GetString(decodedStringInBytes);
         }
         return null;
      }
   }
}
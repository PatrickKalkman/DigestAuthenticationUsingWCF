//-----------------------------------------------------------------------
// <copyright file="Base64Converter.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Text;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for encoding and decoding a base64 string.
   /// </summary>
   internal class Base64Converter
   {
      public virtual string Decode(string base64EncodedString)
      {
         if (base64EncodedString != null)
         {
            byte[] decodedStringInBytes = Convert.FromBase64String(base64EncodedString);
            return Encoding.ASCII.GetString(decodedStringInBytes);
         }

         return null;
      }

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
//-----------------------------------------------------------------------
// <copyright file="MD5Encoder.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for encoding or hashing a string using the
   /// MD5 algorithm.
   /// </summary>
   internal class MD5Encoder
   {
      private readonly MD5 md5 = MD5.Create();

      public virtual string Encode(string stringToEncode)
      {
         if (stringToEncode != null)
         {
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(stringToEncode));
            return ConvertToHexString(hash);
         }

         return null;
      }

      private static string ConvertToHexString(IEnumerable<byte> hash)
      {
         var hexString = new StringBuilder();
         foreach (byte byteFromHash in hash)
         {
            hexString.AppendFormat("{0:x2}", byteFromHash);
         }

         return hexString.ToString();
      }
   }
}
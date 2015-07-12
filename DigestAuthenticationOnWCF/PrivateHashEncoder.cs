//-----------------------------------------------------------------------
// <copyright file="PrivateHashEncoder.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System.Diagnostics;
using System.Globalization;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// The nonce that is returned by the server contains a public part and a private
   /// part. This class is responsible for creating the private part of the nonce. The 
   /// private part is based on a timestamp, the ip-address of the client and a private
   /// key.
   /// </summary>
   internal class PrivateHashEncoder
   {
      private readonly string privateKey;
      private readonly MD5Encoder md5Encoder;

      public PrivateHashEncoder(string privateKey, MD5Encoder md5Encoder)
      {
         this.privateKey = privateKey;
         this.md5Encoder = md5Encoder;
      }

      public virtual string Encode(string dateTimeInMilliSecondsString, string ipAddress)
      {
         Debug.Assert(md5Encoder != null && privateKey != null, "Both members are necessary to be able to encode the hash.");
         if (dateTimeInMilliSecondsString != null)
         {
            string stringToEncode = string.Format(CultureInfo.InvariantCulture, "{0}:{1}:{2}", dateTimeInMilliSecondsString, ipAddress, privateKey);
            return md5Encoder.Encode(stringToEncode);
         }

         return null;
      }
   }
}
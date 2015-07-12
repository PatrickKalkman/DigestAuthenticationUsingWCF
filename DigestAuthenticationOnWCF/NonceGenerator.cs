//-----------------------------------------------------------------------
// <copyright file="NonceGenerator.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Globalization;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for generating a unique nonce.
   /// </summary>
   internal class NonceGenerator
   {
      private readonly PrivateHashEncoder privateHashEncoder;
      private readonly Base64Converter base64Converter;

      public NonceGenerator(PrivateHashEncoder privateHashEncoder, Base64Converter base64Converter)
      {
         this.privateHashEncoder = privateHashEncoder;
         this.base64Converter = base64Converter;
      }

      public virtual string Generate(string ipAddress)
      {
         Debug.Assert(privateHashEncoder != null && base64Converter != null, "Both members are necessary to generate a valid Nonce");
         double dateTimeInMilliSeconds = (DateTime.UtcNow - DateTime.MinValue).TotalMilliseconds;
         string dateTimeInMilliSecondsString = dateTimeInMilliSeconds.ToString(CultureInfo.InvariantCulture);
         string privateHash = privateHashEncoder.Encode(dateTimeInMilliSecondsString, ipAddress);
         string stringToBase64Encode = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", dateTimeInMilliSecondsString, privateHash);
         return base64Converter.Encode(stringToBase64Encode);
      }
   }
}
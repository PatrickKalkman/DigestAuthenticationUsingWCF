//-----------------------------------------------------------------------
// <copyright file="AuthDigestEncoder.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System.Globalization;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for encoding a response when the selected
   /// algorithm is Auth.
   /// </summary>
   internal class AuthDigestEncoder : DigestEncoderBase
   {
      public AuthDigestEncoder(MD5Encoder md5Encoder)
         : base(md5Encoder)
      {
      }

      public override string CreateResponse(DigestHeader digestHeader, string ha1, string ha2)
      {
         return
            md5Encoder.Encode(
               string.Format(
               CultureInfo.InvariantCulture, 
               "{0}:{1}:{2}:{3}:{4}:{5}", 
               ha1, 
               digestHeader.Nonce,
               digestHeader.NounceCounter, 
               digestHeader.Cnonce, 
               digestHeader.Qop.ToString().ToLower(CultureInfo.InvariantCulture), 
               ha2));
      }
   }
}
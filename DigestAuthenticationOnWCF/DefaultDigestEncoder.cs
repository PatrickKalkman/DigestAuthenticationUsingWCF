//-----------------------------------------------------------------------
// <copyright file="DefaultDigestEncoder.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System.Globalization;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for encoding a response when the selected
   /// algorithm is None. This is used the case when the client does not 
   /// support the Auth algorithm. 
   /// </summary>
   internal class DefaultDigestEncoder : DigestEncoderBase
   {
      public DefaultDigestEncoder(MD5Encoder md5Encoder)
         : base(md5Encoder)
      {
      }

      public override string CreateResponse(DigestHeader digestHeader, string ha1, string ha2)
      {
         return md5Encoder.Encode(string.Format(CultureInfo.InvariantCulture, "{0}:{1}:{2}", ha1, digestHeader.Nonce, ha2));
      }
   }
}
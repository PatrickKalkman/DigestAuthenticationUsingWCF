//-----------------------------------------------------------------------
// <copyright file="DigestEncoderBase.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System.Globalization;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// Base class for encoding the username and password of a user. For each algorithm Auth and None
   /// a derived class is created that handles the specialties of that protocol.
   /// </summary>
   internal abstract class DigestEncoderBase
   {
      protected readonly MD5Encoder md5Encoder;

      protected DigestEncoderBase(MD5Encoder md5Encoder)
      {
         this.md5Encoder = md5Encoder;
      }

      public virtual string Encode(DigestHeader digestHeader, string password)
      {
         string ha1 = CreateHa1(digestHeader, password);
         string ha2 = CreateHa2(digestHeader);
         return CreateResponse(digestHeader, ha1, ha2);
      }

      public abstract string CreateResponse(DigestHeader digestHeader, string ha1, string ha2);

      private string CreateHa1(DigestHeader digestHeader, string password)
      {
         return md5Encoder.Encode(string.Format(CultureInfo.InvariantCulture, "{0}:{1}:{2}", digestHeader.UserName, digestHeader.Realm, password));
      }
      
      private string CreateHa2(DigestHeader digestHeader)
      {
         return md5Encoder.Encode(string.Format(CultureInfo.InvariantCulture, "{0}:{1}", digestHeader.Method, digestHeader.Uri));
      }
   }
}
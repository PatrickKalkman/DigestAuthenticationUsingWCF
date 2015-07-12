//-----------------------------------------------------------------------
// <copyright file="DigestEncoders.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class holds a list with all available encoders so that they 
   /// can easily and quickly be selected based on the requested protocol in the 
   /// incoming message from the client.
   /// </summary>
   internal class DigestEncoders : Dictionary<DigestQop, DigestEncoderBase>
   {
      public DigestEncoders(MD5Encoder md5Encoder)
      {
         Add(DigestQop.None, new DefaultDigestEncoder(md5Encoder));
         Add(DigestQop.Auth, new AuthDigestEncoder(md5Encoder));
      }

      public virtual DigestEncoderBase GetEncoder(DigestQop digestQop)
      {
         return this[digestQop];
      }
   }
}
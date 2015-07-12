//-----------------------------------------------------------------------
// <copyright file="NonceValidator.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for validating the nonce that is returned from the client. It should
   /// be the same as the nonce sent by the server and it should not be too old (Stale).
   /// </summary>
   internal class NonceValidator
   {
      private readonly PrivateHashEncoder privateHashEncoder;
      private readonly Base64Converter base64Converter;
      private readonly NonceTimeStampParser nonceTimeStampParser;
      private readonly int staleTimeOutInSeconds;

      public NonceValidator(
         PrivateHashEncoder privateHashEncoder, 
         Base64Converter base64Converter,
         NonceTimeStampParser nonceTimeStampParser, 
         int staleTimeOutInSeconds)
      {
         this.privateHashEncoder = privateHashEncoder;
         this.base64Converter = base64Converter;
         this.nonceTimeStampParser = nonceTimeStampParser;
         this.staleTimeOutInSeconds = staleTimeOutInSeconds;
      }

      public virtual bool Validate(string nonce, string ipAddress)
      {
         string[] decodedParts = GetDecodedParts(nonce);
         string md5EncodedString = privateHashEncoder.Encode(decodedParts[0], ipAddress);
         return string.CompareOrdinal(decodedParts[1], md5EncodedString) == 0;
      }

      public virtual bool IsStale(string nonce)
      {
         string[] decodedParts = GetDecodedParts(nonce);
         DateTime dateTimeFromNonce = nonceTimeStampParser.Parse(decodedParts[0]);
         return dateTimeFromNonce.AddSeconds(staleTimeOutInSeconds) < DateTime.UtcNow;
      }

      private string[] GetDecodedParts(string nonce)
      {
         return base64Converter.Decode(nonce).Split(':');
      }
   }
}
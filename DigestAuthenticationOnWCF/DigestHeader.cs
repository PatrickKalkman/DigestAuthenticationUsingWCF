//-----------------------------------------------------------------------
// <copyright file="DigestHeader.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Text;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for generating the HTTP header in the response
   /// of a Digest Authenticated server.
   /// </summary>
   internal class DigestHeader
   {
      public string Cnonce { get; set; }

      public string Nonce { get; set; }

      public string Opaque { get; set; }

      public DigestAlgorithm Algoritm { get; set; }

      public DigestQop Qop { get; set; }

      public bool? Stale { get; set; }

      public string Realm { get; set; }

      public string Domain { get; set; }

      public string UserName { get; set; }

      public string Uri { get; set; }

      public string Response { get; set; }

      public string Method { get; set; }

      public string NounceCounter { get; set; }

      public string GenerateHeaderString()
      {
         ValidateIfRequiredFieldsAreFilled();

         StringBuilder header = CreateHeader();
         AddRealm(header);
         AddDomain(header);
         AddNonce(header);
         AddOpaque(header);
         AddStale(header);
         AddAlgoritm(header);
         AddQop(header);

         return header.ToString();
      }

      public void Set(HeaderKeyValue headerKeyValue)
      {
         if (headerKeyValue != null)
         {
            if (headerKeyValue.Key == "realm")
            {
               Realm = headerKeyValue.Value;
            }

            if (headerKeyValue.Key == "qop")
            {
               Qop = DigestQop.Parse(headerKeyValue.Value);
            }

            if (headerKeyValue.Key == "nonce")
            {
               Nonce = headerKeyValue.Value;
            }

            if (headerKeyValue.Key == "opaque")
            {
               Opaque = headerKeyValue.Value;
            }

            if (headerKeyValue.Key == "username")
            {
               UserName = headerKeyValue.Value;
            }

            if (headerKeyValue.Key == "response")
            {
               Response = headerKeyValue.Value;
            }

            if (headerKeyValue.Key == "uri")
            {
               Uri = headerKeyValue.Value;
            }

            if (headerKeyValue.Key == "method")
            {
               Method = headerKeyValue.Value;
            }

            if (headerKeyValue.Key == "cnonce")
            {
               Cnonce = headerKeyValue.Value;
            }

            if (headerKeyValue.Key == "nc")
            {
               NounceCounter = headerKeyValue.Value;
            }
         }
      }

      private static StringBuilder CreateHeader()
      {
         return new StringBuilder("Digest ");
      }

      private void AddRealm(StringBuilder header)
      {
         header.AppendFormat("realm=\"{0}\"", Realm);
      }

      private void AddQop(StringBuilder header)
      {
         if (Qop != null && Qop != DigestQop.None)
         {
            header.AppendFormat(", qop=\"{0}\"", ConvertQopToString());
         }
      }

      private void AddAlgoritm(StringBuilder header)
      {
         if (Algoritm != null && Algoritm != DigestAlgorithm.None)
         {
            header.AppendFormat(", algorithm=\"{0}\"", Algoritm);
         }
      }

      private void AddStale(StringBuilder header)
      {
         if (Stale.HasValue)
         {
            header.AppendFormat(", stale=\"{0}\"", ConvertStaleToString());
         }
      }

      private void AddOpaque(StringBuilder header)
      {
         if (!string.IsNullOrEmpty(Opaque))
         {
            header.AppendFormat(", opaque=\"{0}\"", Opaque);
         }
      }

      private void AddNonce(StringBuilder header)
      {
         header.AppendFormat(", nonce=\"{0}\"", Nonce);
      }

      private void AddDomain(StringBuilder header)
      {
         if (!string.IsNullOrEmpty(Domain))
         {
            header.AppendFormat(", domain=\"{0}\"", Domain);
         }
      }

      private void ValidateIfRequiredFieldsAreFilled()
      {
         if (string.IsNullOrEmpty(Realm))
         {
            throw new InvalidOperationException("Set the Realm property before generating the header");
         }

         if (string.IsNullOrEmpty(Nonce))
         {
            throw new InvalidOperationException("Set the Nonce property before generating the header");
         }
      }

      private string ConvertStaleToString()
      {
         if (Stale.HasValue)
         {
            return Stale.Value ? "true" : "false";
         }

         throw new InvalidOperationException("Stale has no value, no header could be generated.");
      }

      private string ConvertQopToString()
      {
         return Qop.ToString().ToLowerInvariant();
      }
   }
}
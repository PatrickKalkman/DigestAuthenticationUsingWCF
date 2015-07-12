//-----------------------------------------------------------------------
// <copyright file="DigestQop.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Globalization;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This enumeration is used by client to specify the quality of protection (Qop) that
   /// is whishes to use.
   /// </summary>
   internal class DigestQop : EnumWithToString
   {
      public static readonly DigestQop None = new DigestQop("None");
      public static readonly DigestQop Auth = new DigestQop("Auth");
      public static readonly DigestQop AuthInt = new DigestQop("Auth-int");
      public static readonly DigestQop Token = new DigestQop("Token");

      private DigestQop(string description)
         : base(description)
      {
      }

      public static DigestQop Parse(string value)
      {
         switch (value.ToLower(CultureInfo.InvariantCulture))
         {
            case "none":
               return None;
            case "auth":
               return Auth;
            case "auth-int":
               return AuthInt;
            case "token":
               return Token;
            default:
               throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The given value {0} cannot be converted to a DigestQop", value));
         }
      }
   }
}
//-----------------------------------------------------------------------
// <copyright file="DigestAlgorithm.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This enum is used to specify the algorithm that is used by
   /// the server to encode the messages.
   /// </summary>
   internal class DigestAlgorithm : EnumWithToString
   {
      public static readonly DigestAlgorithm None = new DigestAlgorithm("None");
      public static readonly DigestAlgorithm MD5 = new DigestAlgorithm("MD5");
      public static readonly DigestAlgorithm MD5Sess = new DigestAlgorithm("MD5-sess");

      private DigestAlgorithm(string description)
         : base(description)
      {
      }
   }
}
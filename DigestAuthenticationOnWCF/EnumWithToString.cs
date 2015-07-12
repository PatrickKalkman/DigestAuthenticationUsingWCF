//-----------------------------------------------------------------------
// <copyright file="EnumWithToString.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// Helper class to be able to create an enumeration with 
   /// a name and a separate description.
   /// </summary>
   internal class EnumWithToString
   {
      private readonly string description;

      internal EnumWithToString(string description)
      {
         this.description = description;
      }

      public override string ToString()
      {
         return description;
      }
   }
}
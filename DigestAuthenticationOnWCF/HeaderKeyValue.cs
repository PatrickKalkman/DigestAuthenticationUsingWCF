//-----------------------------------------------------------------------
// <copyright file="HeaderKeyValue.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// Class to hold a key and a value from the incoming message header.
   /// </summary>
   internal class HeaderKeyValue
   {
      public HeaderKeyValue(string key, string value)
      {
         Key = key;
         Value = value;
      }

      public string Key { get; set; }

      public string Value { get; set; }
   }
}
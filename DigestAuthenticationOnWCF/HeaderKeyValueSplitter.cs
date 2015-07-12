//-----------------------------------------------------------------------
// <copyright file="HeaderKeyValueSplitter.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for splitting a single key value string from 
   /// the incoming message header.
   /// </summary>
   internal class HeaderKeyValueSplitter
   {
      private readonly char splitCharacter;

      public HeaderKeyValueSplitter(char splitCharacter)
      {
         this.splitCharacter = splitCharacter;
      }

      public HeaderKeyValue Split(string keyValuePair)
      {
         if (!string.IsNullOrEmpty(keyValuePair))
         {
            string[] keyValues = keyValuePair.Trim().Split(new char[] { splitCharacter }, 2);
            if (keyValues.Length == 2)
            {
               return new HeaderKeyValue(keyValues[0], keyValues[1].Replace("\"", string.Empty));
            }
         }

         return null;
      }
   }
}
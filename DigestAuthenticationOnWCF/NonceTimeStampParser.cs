//-----------------------------------------------------------------------
// <copyright file="NonceTimeStampParser.cs" company="http://www.semanticarchitecture.net">
//     (C) Patrick Kalkman pkalkie@gmail.com
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Globalization;

namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for parsing the date time stamp that is received 
   /// from the client.
   /// </summary>
   internal class NonceTimeStampParser
   {
      public DateTime Parse(string nonceTimeStamp)
      {
         double nonceTimeStampDouble;
         if (Double.TryParse(nonceTimeStamp, NumberStyles.Float, CultureInfo.InvariantCulture, out nonceTimeStampDouble))
         {
            return DateTime.MinValue.AddMilliseconds(nonceTimeStampDouble);
         }

         throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The given nonce time stamp {0} was not valid", nonceTimeStamp));
      }
   }
}
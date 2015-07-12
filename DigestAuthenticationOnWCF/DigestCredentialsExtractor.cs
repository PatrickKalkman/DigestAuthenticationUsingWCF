namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class is responsible for extracting and decoding the 
   /// credentials from the encoded authorization string.
   /// </summary>
   public class DigestHeaderExtractor
   {
      private readonly Base64Decoder base64Decoder;
      private readonly DigestHeaderParser digestHeaderParser;

      internal DigestHeaderExtractor(Base64Decoder base64Decoder, DigestHeaderParser digestHeaderParser)
      {
         this.base64Decoder = base64Decoder;
         this.digestHeaderParser = digestHeaderParser;
      }

      internal virtual DigestHeader Extract(string digestAuthenticationCredentials)
      {
         return digestHeaderParser.Parse(digestAuthenticationCredentials);
      }
   }
}
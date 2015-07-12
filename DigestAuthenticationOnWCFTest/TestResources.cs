using System;
using System.ServiceModel.Channels;
using DigestAuthenticationOnWCF;

namespace DigestAuthenticationOnWCFTest
{
   internal class TestResources
   {
      public static readonly string PrivateKey = "MyPrivateKey";

      public static readonly string GeneratedValidNonce = "NjM0MzM4ODg3Nzc1MzEuMzpmZDIxNzllOTUzMDY2ODc2YWQyYjY1NmVmZGJkYTc4MQ==";

      public const long NanceValidPeriodInSeconds = 300;

      public static readonly DateTime NonceTimeStampAsDateTime = new DateTime(2010, 06, 24, 10, 23, 45, 5);

      public static readonly string NonceTimeStampAsString = "63412971825005";

      public static readonly string GeneratedValidResponse = "6d2c6ad771845799dbf3e4ed88b9d8a4";

      public static readonly string Password = "Wever";

      public static readonly string IpAddress = "192.168.16.1";

      public static readonly string Realm = "MyRealm";

      public static readonly string AuthenticationString = "Digest realm=\"testrealm@host.com\", qop=\"auth,auth-int\", nonce=\"dcd98b7102dd2f0e8b11d0f600bfb0c093\", opaque=\"5ccc069c403ebaf9f0171e9517f40e41\"";

      public static readonly char HeaderKeyValueSplitCharacter = '=';

      public static Message CreateMessage(string ipAddress, int port)
      {
         Message requestMessage = Message.CreateMessage(MessageVersion.None, null);
         var remoteEndPointProperty = new RemoteEndpointMessageProperty(ipAddress, port);
         requestMessage.Properties.Add(RemoteEndpointMessageProperty.Name, remoteEndPointProperty);
         return requestMessage;
      }

      public static Message CreateMessage()
      {
         const string AuthenticationHeaderName = "Authorization";
         Message requestMessage = Message.CreateMessage(MessageVersion.None, null);
         var requestProperty = new HttpRequestMessageProperty();
         requestProperty.Headers.Add(AuthenticationHeaderName, AuthenticationString);
         requestProperty.Method = "GET";
         requestMessage.Properties.Add(HttpRequestMessageProperty.Name, requestProperty);
         return requestMessage;
      }

      public static DigestHeader CreateHeader()
      {
         var header = new DigestHeader();
         header.Method = "GET";
         header.Nonce = GeneratedValidNonce;
         return header;
      }
   }
}
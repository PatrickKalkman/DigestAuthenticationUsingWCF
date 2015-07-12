using System.ServiceModel.Channels;
using DigestAuthenticationOnWCF;
using NUnit.Framework;
using Rhino.Mocks;

namespace DigestAuthenticationOnWCFTest
{
   [TestFixture]
   public class DigestAuthenticationInterceptorTest
   {
      [Test]
      public void ShouldAuthenticateRequestUsingAuthenticationManager()
      {
         var repository = new MockRepository();
         var context = repository.Stub<RequestContext>();
         var manager = repository.StrictMock<DigestAuthenticationManager>(null, null, null, null, null, null, null, null);
         using (repository.Record())
         {
            manager.Expect(mgr => mgr.AuthenticateRequest(null)).IgnoreArguments().Return(false);
            manager.Expect(mgr => mgr.CreateInvalidAuthenticationRequest(null)).IgnoreArguments();
         }
         var digestAuthenticationInterceptor = new DigestAuthenticationInterceptor(manager);
         using (repository.Playback())
         {
            digestAuthenticationInterceptor.ProcessRequest(ref context);
         }
         repository.VerifyAll();
      }
   }
}
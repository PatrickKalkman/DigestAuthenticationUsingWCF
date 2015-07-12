using System.Web.Security;
using DigestAuthenticationOnWCF;
using Rhino.Mocks;

namespace DigestAuthenticationOnWCFTest
{
   internal class DigestAuthenticationManagerFactory
   {
      protected readonly MockRepository repository;
      protected ResponseMessageFactory factory;
      protected IPAddressExtractor ipAddressExtractor;
      protected DigestHeaderExtractor digestHeaderExtractor;
      protected MembershipProvider provider;
      protected ServiceSecurityContextFactory serviceSecurityContextFactory;
      protected NonceValidator nonceValidator;
      protected DigestValidator digestValidator;
      protected DigestHeaderFactory digestHeaderGenerator;

      public DigestAuthenticationManagerFactory(MockRepository repository)
      {
         this.repository = repository;
      }

      public ResponseMessageFactory Factory
      {
         get { return factory; }
      }

      public IPAddressExtractor IpAddressExtractor
      {
         get { return ipAddressExtractor; }
      }

      public DigestHeaderExtractor DigestHeaderExtractor
      {
         get { return digestHeaderExtractor; }
      }

      public MembershipProvider Provider
      {
         get { return provider; }
      }

      public ServiceSecurityContextFactory ServiceSecurityContextFactory
      {
         get { return serviceSecurityContextFactory; }
      }

      public NonceValidator NonceValidator
      {
         get { return nonceValidator; }
      }

      public DigestValidator DigestValidator
      {
         get { return digestValidator; }
      }

      public DigestHeaderFactory DigestHeaderGenerator
      {
         get { return digestHeaderGenerator; }
      }

      public DigestAuthenticationManager Create()
      {
         factory = CreateResponseMessageFactory();
         ipAddressExtractor = CreateIpAddressExtractor();
         provider = CreateMembershipProvider();
         digestHeaderExtractor = CreateDigestHeaderExtractor();
         serviceSecurityContextFactory = CreateServiceSecurityContextFactory();
         nonceValidator = CreateNonceValidator();
         digestValidator = CreateDigestValidator();
         digestHeaderGenerator = CreateDigestHeaderGenerator();

         return new DigestAuthenticationManager(
            factory,
            ipAddressExtractor,
            provider,
            digestHeaderExtractor,
            serviceSecurityContextFactory,
            nonceValidator,
            digestValidator,
            digestHeaderGenerator);
      }

      public virtual ResponseMessageFactory CreateResponseMessageFactory()
      {
         return MockCreator<ResponseMessageFactory>.Stub(repository);
      }

      public virtual IPAddressExtractor CreateIpAddressExtractor()
      {
         return MockCreator<IPAddressExtractor>.Stub(repository);
      }

      public virtual MembershipProvider CreateMembershipProvider()
      {
         return MockCreator<MembershipProvider>.Stub(repository);
      }

      public virtual DigestHeaderExtractor CreateDigestHeaderExtractor()
      {
         return MockCreator<DigestHeaderExtractor>.Stub(repository);
      }

      public virtual ServiceSecurityContextFactory CreateServiceSecurityContextFactory()
      {
         return MockCreator<ServiceSecurityContextFactory>.Stub(repository);
      }

      public virtual NonceValidator CreateNonceValidator()
      {
         return MockCreator<NonceValidator>.Stub(repository);
      }

      public virtual DigestValidator CreateDigestValidator()
      {
         return MockCreator<DigestValidator>.Stub(repository);
      }

      public virtual DigestHeaderFactory CreateDigestHeaderGenerator()
      {
         return MockCreator<DigestHeaderFactory>.Stub(repository);
      }
   }
}
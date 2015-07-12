using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Microsoft.ServiceModel.Web;

namespace WCFServer
{
   /// <summary>
   /// This class is responsible for creating a servicehost that includes a basic 
   /// authentication request interceptor.
   /// </summary>
   public class DigestAuthenticationHostFactory : ServiceHostFactory
   {
      protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
      {
         var serviceHost = new WebServiceHost2(serviceType, true, baseAddresses);
         RequestInterceptor interceptor = DigestAuthenticationOnWCF.RequestInterceptorFactory.Create("DataWebService", "MyPrivateKey", new CustomMembershipProvider());
         serviceHost.Interceptors.Add(interceptor);
         return serviceHost;
      }
   }
}
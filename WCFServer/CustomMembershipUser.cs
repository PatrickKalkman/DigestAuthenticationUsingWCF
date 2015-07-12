using System.Web.Security;

namespace WCFServer
{
   internal class CustomMembershipUser : MembershipUser
   {
      public override string GetPassword()
      {
         return "Tjoepje";
      }
   }
}
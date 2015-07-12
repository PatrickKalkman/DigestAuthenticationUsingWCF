namespace DigestAuthenticationOnWCF
{
   /// <summary>
   /// This class hold the credentials of a user.
   /// </summary>
   public class Credentials
   {
      private readonly string userName;
      private readonly string password;

      public Credentials(string userName, string password)
      {
         this.userName = userName;
         this.password = password;
      }

      public string UserName
      {
         get { return userName; }
      }

      public string Password
      {
         get { return password; }
      }
   }
}
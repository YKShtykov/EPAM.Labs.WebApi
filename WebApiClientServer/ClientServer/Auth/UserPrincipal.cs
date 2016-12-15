using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ClientServer.Auth
{  
  public class UserPrincipal : IPrincipal
  {
    public IIdentity Identity { get; private set; }
    public bool IsInRole(string role) { return false; }

    public UserPrincipal(string login)
    {
      Identity = new GenericIdentity(login);
    }
    public string Login { get; set; }
  }

  public class UserPrincipalSerializeModel
  {
    public string Login { get; set; }
  }
}
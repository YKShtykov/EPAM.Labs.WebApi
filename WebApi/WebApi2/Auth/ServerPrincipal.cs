using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebApi2.Auth
{
  public class ServerPrincipal: IPrincipal
  {
    public IIdentity Identity { get; private set; }
    public bool IsInRole(string role) { return false; }

    public ServerPrincipal(string code)
    {
      Identity = new GenericIdentity(code);
    }
    public string CodeNumber { get; set; }
  }

  public class ServerPrincipalSerializeModel
  {
    public string codeNumber { get; set; }
  }
}

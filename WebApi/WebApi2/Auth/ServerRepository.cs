using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi2.Auth
{
  public class ServerRepository
  {
    private ServerContext db = new ServerContext();

    public Server FindByPredicate(Func<Server, bool> predicate)
    {
      return db.Servers.FirstOrDefault(predicate);
    }
  }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApi2.Auth
{
  public class ServerDbInitializer : DropCreateDatabaseAlways<ServerContext>
  {
    protected override void Seed(ServerContext db)
    {
      db.Servers.Add(new Server() { Id=1, CodeNumber="1111",Password= "admin" });

      base.Seed(db);
    }
  }
}
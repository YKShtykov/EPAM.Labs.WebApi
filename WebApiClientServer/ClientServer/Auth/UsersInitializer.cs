using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClientServer.Auth
{
  public class UsersInitializer: DropCreateDatabaseAlways<UserContext>
  {
    protected override void Seed(UserContext db)
    {
      db.Users.Add(new User(1,"admin", "admin"));

      base.Seed(db);
    }
  }
}
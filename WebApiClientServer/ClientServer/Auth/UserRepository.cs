using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientServer.Auth
{
  public class UserRepository
  {
    private UserContext db = new UserContext();

    public User FindByPredicate( Func<User,bool> predicate)
    {
      return db.Users.FirstOrDefault(predicate);
    }
  }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClientServer.Auth
{
  public class UserContext: DbContext
  {
    public DbSet<User> Users { get; set; }
  }
}
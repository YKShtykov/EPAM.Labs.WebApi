using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApi2.Auth
{
  public class ServerContext:DbContext
  {
    public DbSet<Server> Servers { get; set; }
  }
}
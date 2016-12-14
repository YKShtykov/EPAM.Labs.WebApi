using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApi2.Models;

namespace WebApi2.Contexts
{
    public class ItemContext: DbContext
    {
        public DbSet<Item> Items { get; set; }
    }
}
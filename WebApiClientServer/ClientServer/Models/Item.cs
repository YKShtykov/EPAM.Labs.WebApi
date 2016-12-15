using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientServer.Models
{
  public class Item
  {
    public int Id { get; set; }
    public string Caption { get; set; }
    public string Text { get; set; }
    public string Author { get; set; }
  }
}
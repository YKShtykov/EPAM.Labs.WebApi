using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApi2.Contexts;

namespace WebApi2.Models
{
    public class ItemDbInitializer: DropCreateDatabaseAlways<ItemContext>
    {
        protected override void Seed(ItemContext db)
        {
            db.Items.Add(new Item { Caption = "task 1", Author = "Л. Толстой", Text="....", Id=1 });
            db.Items.Add(new Item { Caption = "task 2", Author = "И. Тургенев", Text = "....", Id = 2 });
            db.Items.Add(new Item { Caption = "task 3", Author = "А. Чехов", Text = "....", Id = 3 });

            base.Seed(db);
        }
    }
}
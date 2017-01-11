namespace ResturantDemo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ResturantDemo.Models;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<ResturantDemo.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ResturantDemo.Models.ApplicationDbContext context)
        {

            var winList = new Category { Name = "Wines" };
            //  This method will be called after migrating to the latest version.
            context.Categories.AddOrUpdate(cat => cat.Name,
               winList,
                new Models.Category { Name = "Desserts" },
                new Models.Category { Name = "Entrees" },
                new Models.Category { Name = "Appetizers" }
                );

            context.SaveChanges();

            var wine1 = new MenuItem { Name = "Riseling", Price = 2.45, CategoryId = winList.Id };
            var wine2 = new MenuItem { Name = "Chardenauy", Price = 10.12, CategoryId = winList.Id };
            context.MenuItems.AddOrUpdate(mi => mi.Name,
                wine1, 
                wine2);
        }
    }
}

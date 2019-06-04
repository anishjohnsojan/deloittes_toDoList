namespace Deloitte_ToDoList9.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Deloitte_ToDoList9.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Deloitte_ToDoList9.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Deloitte_ToDoList9.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            AddUsers(context);
        }

        void AddUsers(Deloitte_ToDoList9.Models.ApplicationDbContext context)
        {
            var user = new ApplicationUser { UserName = "test" };
            var user_manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            user_manager.Create(user, "pwd123");
        }
    }
}

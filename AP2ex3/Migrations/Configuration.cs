namespace AP2ex3.Migrations
{
    using AP2ex3.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AP2ex3.Models.AP2ex3Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AP2ex3Context context)
        {
            new User() { UserName = "yarden", Email = "y@g.c", Password = "123" };
            
        }
    }
    }


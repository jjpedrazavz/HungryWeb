namespace HungryWeb.Migrations
{
    using HungryWeb.Context;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HungryWeb.Context.StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HungryWeb.Context.StoreContext context)
        {
             
            
        }
    }
}

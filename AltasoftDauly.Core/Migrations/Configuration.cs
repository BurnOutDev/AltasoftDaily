namespace AltasoftDaily.Core.Migrations
{
    using Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AltasoftDailyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AltasoftDaily.Core.AltasoftDailyContext context)
        {
            context.Users.AddOrUpdate(
                new User { Name = "არტურ", LastName = "თუმანიანი" },
                new User { Name = "დაჩი", LastName = "სახოკია" },
                new User { Name = "ირაკლი", LastName = "ირემაშვილი" },
                new User { Name = "გიორგი", LastName = "ქავთარაძე" },
                new User { Name = "შიო", LastName = "ხელაძე" },
                new User { Name = "გიორგი", LastName = "ბირკაია" },
                new User { Name = "ლუკა", LastName = "ზიბზიბაძე" },
                new User { Name = "გურამ", LastName = "წიკლაური" },
                new User { Name = "ლაშა", LastName = "ჩაჩანიძე" },
                new User { Name = "ბექა", LastName = "ელისაშვილი" });

            context.SaveChanges();
        }
    }
}
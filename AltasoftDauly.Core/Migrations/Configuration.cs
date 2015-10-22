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
                new User { Name = "არტურ", LastName = "თუმანიანი", AltasoftUserID=13, DeptId = 2, Username = "atumaniani", Password = "123456" },
                new User { Name = "დაჩი", LastName = "სახოკია", AltasoftUserID = 14, DeptId = 3, Username = "dsakhokia", Password = "123456" },
                new User { Name = "ირაკლი", LastName = "ირემაშვილი", AltasoftUserID = 23, DeptId = 6, Username = "iiremashvili", Password = "123456" },
                new User { Name = "გიორგი", LastName = "ქავთარაძე", AltasoftUserID = 24, DeptId = 7, Username = "gkavtaradze", Password = "123456" },
                new User { Name = "შიო", LastName = "ხელაძე", AltasoftUserID = 25, DeptId = 1, Username = "shkheladze", Password = "123456" },
                new User { Name = "გიორგი", LastName = "ბირკაია", AltasoftUserID = 26, DeptId = 6, Username = "gbirkaia", Password = "123456" },
                new User { Name = "ლუკა", LastName = "ზიბზიბაძე", AltasoftUserID = 27, DeptId = 5, Username = "lzibzibadze", Password = "123456" },
                new User { Name = "გურამ", LastName = "წიკლაური", AltasoftUserID = 29, DeptId = 1, Username = "gtsiklauri", Password = "123456" },
                new User { Name = "ლაშა", LastName = "ჩაჩანიძე", AltasoftUserID = 30, DeptId = 7, Username = "lchachanidze", Password = "123456" },
                new User { Name = "ბექა", LastName = "ელისაშვილი", AltasoftUserID = 31, DeptId = 1, Username = "belisashvili", Password = "123456" });

            context.SaveChanges();
        }
    }
}
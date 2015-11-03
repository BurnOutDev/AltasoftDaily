namespace AltasoftDaily.Core.Migrations
{
    using AltasoftDaily.Domain.POCO;
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
                new User { Name = "არტურ", LastName = "თუმანიანი", AltasoftUserID = 13, DeptID = 2, Username = "atumaniani", Password = "123456", Filter = new Filter(), PrivateNumber = "01030036753" },
                new User { Name = "დაჩი", LastName = "სახოკია", AltasoftUserID = 14, DeptID = 3, Username = "dsakhokia", Password = "123456", Filter = new Filter(), PrivateNumber = "01027027684" },
                new User { Name = "ირაკლი", LastName = "ირემაშვილი", AltasoftUserID = 23, DeptID = 6, Username = "iiremashvili", Password = "123456", Filter = new Filter(), PrivateNumber = "36001048182" },
                new User { Name = "გიორგი", LastName = "ქავთარაძე", AltasoftUserID = 24, DeptID = 7, Username = "gkavtaradze", Password = "123456", Filter = new Filter(), PrivateNumber = "13001061954" },
                new User { Name = "შიო", LastName = "ხელაძე", AltasoftUserID = 25, DeptID = 1, Username = "shkheladze", Password = "123456", Filter = new Filter(), PrivateNumber = "01024064244" },
                new User { Name = "გიორგი", LastName = "ბირკაია", AltasoftUserID = 26, DeptID = 6, Username = "gbirkaia", Password = "123456", Filter = new Filter(), PrivateNumber = "01030032939" },
                new User { Name = "ლუკა", LastName = "ზიბზიბაძე", AltasoftUserID = 27, DeptID = 5, Username = "lzibzibadze", Password = "123456", Filter = new Filter(), PrivateNumber = "01024074538" },
                new User { Name = "გურამ", LastName = "წიკლაური", AltasoftUserID = 29, DeptID = 1, Username = "gtsiklauri", Password = "123456", Filter = new Filter(), PrivateNumber = "01005032786" },
                new User { Name = "ლაშა", LastName = "ჩაჩანიძე", AltasoftUserID = 30, DeptID = 7, Username = "lchachanidze", Password = "123456", Filter = new Filter(), PrivateNumber = "01027059129" },
                new User { Name = "ბექა", LastName = "ელისაშვილი", AltasoftUserID = 31, DeptID = 1, Username = "belisashvili", Password = "123456", Filter = new Filter(), PrivateNumber = "01008039538" });

            context.SaveChanges();
        }
    }
}
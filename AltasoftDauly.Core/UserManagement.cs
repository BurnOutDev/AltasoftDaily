﻿using AltasoftDaily.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Core
{
    public static class UserManagement
    {
        public static User Authenticate(string username, string password, out bool authenticated, out string message)
        {
            using (var db = new AltasoftDailyContext())
            {
                var userByName = db.Users.FirstOrDefault(x => x.Username == username);

                if (userByName == null)
                {
                    authenticated = false;
                    message = "მომხმარებლის სახელი არასწორია!";
                    return null;
                }
                else if (userByName.Password != password)
                {
                    authenticated = false;
                    message = "მომხმარებლის პაროლი არასწორია!";
                    return null;
                }

                authenticated = true;
                message = "მომხმარებელი იდენტიფიცირებულია!";
                return userByName;
            }
        }

        public static bool ChangePassword(User user, string oldPassword, string newPassword)
        {
            using (var db = new AltasoftDailyContext())
            {
                var dbUser = db.Users.FirstOrDefault(x => x.UserID == user.UserID);

                if (dbUser.Password == oldPassword)
                {
                    user.Password = newPassword;
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
        }
    }
}

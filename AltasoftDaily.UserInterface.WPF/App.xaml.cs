using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AltasoftDaily.Domain.POCO;

namespace AltasoftDaily.UserInterface.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public User User { get; set; }
        public bool _userLogged { get; set; }
        public bool UserLoggedIn
        {
            get
            {
                if (User != null)
                    return _userLogged;
                _userLogged = false;
                return false;
            }
        }
    }
}

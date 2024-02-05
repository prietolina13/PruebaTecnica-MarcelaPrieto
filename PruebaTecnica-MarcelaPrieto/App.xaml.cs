﻿using System.Configuration;
using System.Data;
using System.Windows;
using PruebaTecnica_MarcelaPrieto.View;

namespace PruebaTecnica_MarcelaPrieto
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart (Object sender, StartupEventArgs e)
        {
            var loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new MainWindow();
                    mainView.Show();
                    loginView.Close();
                }
            };
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PruebaTecnica_MarcelaPrieto.CustomControl
{
    /// <summary>
    /// Interaction logic for PasswordInput.xaml
    /// </summary>
    public partial class PasswordInput : UserControl
    {

        public static readonly DependencyProperty PasswordField =
            DependencyProperty.Register("Password", typeof(string),typeof(PasswordInput));

        public string Password
        {
            get { return (string)GetValue(PasswordField); }
            set { SetValue(PasswordField, value); }
        }
        public PasswordInput()
        {
            InitializeComponent();
            txtPassword.PasswordChanged += PasswordChange;
        }

        private void PasswordChange(object sender, RoutedEventArgs e)
        {
            Password=txtPassword.Password;
        }
    }
}

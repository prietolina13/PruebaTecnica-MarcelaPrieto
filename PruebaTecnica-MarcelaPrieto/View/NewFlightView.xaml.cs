using PruebaTecnica_MarcelaPrieto.ViewModel;
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
using System.Windows.Shapes;

namespace PruebaTecnica_MarcelaPrieto.View
{
    /// <summary>
    /// Interaction logic for NewFlightView.xaml
    /// </summary>
    public partial class NewFlightView : Window
    {
        public NewFlightView()
        {
            InitializeComponent();
        }
        private void Allow_Window_Movement(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Minimize_Window(Object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Close_Window(Object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CloseWindowCancel(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();

            Close();
        }

        

    }
}

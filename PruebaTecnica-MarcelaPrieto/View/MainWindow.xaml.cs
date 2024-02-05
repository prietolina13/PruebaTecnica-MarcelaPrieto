using PruebaTecnica_MarcelaPrieto.Model;
using PruebaTecnica_MarcelaPrieto.View;
using PruebaTecnica_MarcelaPrieto.ViewModel;
using System.Reflection;
using System.Security.RightsManagement;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PruebaTecnica_MarcelaPrieto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
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

        private void OpenNewFlight(object sender, RoutedEventArgs e)
        {

            NewFlightView newFlightWindow = new NewFlightView();

            newFlightWindow.ShowDialog();
        }    
        

    }
}
using PruebaTecnica_MarcelaPrieto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PruebaTecnica_MarcelaPrieto.Repositorie;
using System.Security.Principal;
using System.Net;

namespace PruebaTecnica_MarcelaPrieto.ViewModel
{
    public class LoginViewModel: ViewModelBase
    {
        private string _userName;
        private string _password;
        private string _errorMessage;
        private bool _success = true;

        private IUserRepository _userRepository;

        //Métodos
        public string UserName
        {
            get{return _userName;}
            set { _userName = value; OnPropertyChanged(nameof(UserName));}
        }

        public string Password
        {
            get { return _password;}
            set { _password = value; OnPropertyChanged(nameof(Password));}
        }

        public string ErrorMessage
        {
            get { return _errorMessage;}
            set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage));}
        }

        public bool Success
        {
            get { return _success;}
            set { _success = value; OnPropertyChanged(nameof(Success)); }
        }

        //Definición de comando para iniciar sesión
        public ICommand LoginCommand { get; }

        //Constructor

        public LoginViewModel()
        {
            _userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool correctData;

            try
            {
                if (string.IsNullOrWhiteSpace(UserName) || UserName.Length < 5 || Password == null || Password.Length < 3)
                {
                    correctData = false;
                }else
                {
                    correctData = true;
                }
                return correctData;
            }catch (Exception e)
            {
                Console.WriteLine($"Error al invocar CanExecuteLoginComma: {e.Message}");
                return false;
            }
        }
            
        

        private void ExecuteLoginCommand(object obj)
        {
            var isValid = _userRepository.Authentication(new NetworkCredential(UserName, Password));
            if (isValid)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(UserName), null);
                Success = false;
            }
            else
            {
                ErrorMessage = "Nombre de usuario o contraseña inválidos";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PruebaTecnica_MarcelaPrieto.ViewModel
{
    public abstract class ViewModelBase: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Metodo que permite notificar cambios que ocurran en las propiedades a la Interfaz 
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al invocar PropertyChanged para la propiedad {propertyName}: {e.Message}");
            }
        }
        }
    }


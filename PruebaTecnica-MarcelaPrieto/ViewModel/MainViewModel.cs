using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using PruebaTecnica_MarcelaPrieto.Model;
using PruebaTecnica_MarcelaPrieto.Repositorie;

namespace PruebaTecnica_MarcelaPrieto.ViewModel
{
    public class MainViewModel: ViewModelBase
    {
        //Listado de vuelos, ciudades, aerolíneas y estados de vuelo
        private List<OutboundFlightsModel> _outboundFlights;
        private List<CityModel> _city;
        private List<AirlineModel> _airline;
        private List<FlightStatusModel> _flightStatus;

        //Objetos usados para registro y  para la obtención de datos del formulario        
        private CityModel _selectedCity;
        private CityModel _selectedCityDestinarion;
        private AirlineModel _selectedAirline;
        private FlightStatusModel _selectedFlightStatus;
        private OutboundFlightsModel _rowSelected;
        private NewFlightModel _newFlight;

        private int _cityOrigin;
        private int _cityDestination;
        private string _timeDeparture;
        private string _timeArrival;
        private string _flightNumber;
        private int _airlineForm;
        private int _flightStatusForm;
        private string _message;



        //Interfaces de repositorios
        private IOutboundFlightsRepository _outboundFlightsRepository;
        private IAirlineRepository _airlineRepository;
        private IFlightStatusRepository _flightStatusRepository;
        private ICityRepository _cityRepository;
               

        //Propiedades
        public List<OutboundFlightsModel> OutboundFlights
        {
            get { return _outboundFlights; }
            set { _outboundFlights = value; OnPropertyChanged(nameof(OutboundFlights)); }
        }

        public List<CityModel> City
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(nameof(City)); }
        }

        public List<AirlineModel> Airline
        {
            get { return _airline; }
            set { _airline = value; OnPropertyChanged(nameof(Airline)); }
        }

        public List<FlightStatusModel> FlightStatus
        {
            get { return _flightStatus; }
            set { _flightStatus = value; OnPropertyChanged(nameof(FlightStatus)); } 
        }

      
        public CityModel SelectedCity
        {
            get { return _selectedCity; }
            set { _selectedCity = value; OnPropertyChanged(nameof(SelectedCity)); }
        }
        public CityModel SelectedCityDestinarion
        {
            get { return _selectedCityDestinarion; }
            set { _selectedCityDestinarion = value; OnPropertyChanged(nameof(SelectedCityDestinarion)); }
        }

        public AirlineModel SelectedAirline
        {
            get { return _selectedAirline; }
            set { _selectedAirline = value; OnPropertyChanged(nameof(SelectedAirline)); }
        }
        public FlightStatusModel SelectedFlightStatus

        {
            get { return _selectedFlightStatus; }
            set { _selectedFlightStatus = value; OnPropertyChanged(nameof(SelectedFlightStatus)); }
        }

        public OutboundFlightsModel RowSelected
        {
            get { return _rowSelected; }
            set { _rowSelected = value; OnPropertyChanged(nameof(RowSelected)); }
        }
        public NewFlightModel NewFlight
        {
            get { return _newFlight; }
            set { _newFlight = value; OnPropertyChanged(nameof(NewFlight)); }
        }


        public int CityOrigin
        {
            get { return _cityOrigin; }
            set { _cityOrigin = value; OnPropertyChanged(nameof(CityOrigin)); }

        }
        public int CityDestination
        {
            get { return _cityDestination; }
            set { _cityDestination = value; OnPropertyChanged(nameof(CityDestination)); }
        }
        public string TimeDeparture
        {
            get { return _timeDeparture; }
            set { _timeDeparture = value; OnPropertyChanged(nameof(TimeDeparture));}
        }
        public string TimeArrival
        {
            get { return _timeArrival; }
            set { _timeArrival = value; OnPropertyChanged(nameof(TimeArrival)); }
        }
        public string FlightNumber
        {
            get { return _flightNumber; }
            set { _flightNumber = value; OnPropertyChanged(nameof(FlightNumber)); }
        }

        public int AirlineForm
        {
            get { return _airlineForm; }
            set { _airlineForm = value; OnPropertyChanged(nameof(AirlineForm)); }
        }
        public int FlightStatusForm
        {
            get { return _flightStatusForm; }
            set { _flightStatusForm = value; OnPropertyChanged(nameof(FlightStatusForm)); }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(nameof(Message)); }
        }

        //Comandos
        public ICommand ScheduleFlight { get; }
        public ICommand DeteleFlight { get; }

        //Constructor
        public MainViewModel()
        {

            ShowFlights();
            GetAllCities();
            GetAllAirlines();
            GetAllStatus();

           
            //Inicialización de interfaces
            _outboundFlightsRepository = new OutboundFlightsRepository();
            _airlineRepository = new AirlieneRepository();
            _flightStatusRepository = new FlightStatusRepository();
            _cityRepository = new CityRepository();
            RowSelected = new OutboundFlightsModel();

            //Inicialización de los comandos
            ScheduleFlight = new ViewModelCommand(ExecuteScheduleFlight, CanExecuteScheduleFlight);
            DeteleFlight = new ViewModelCommand(ExecuteDeteleFlight);
        }

        
        

        private void ExecuteDeteleFlight(Object obj)
        {
             
            var deletedFlight = _outboundFlightsRepository.LogicalDelete(RowSelected.FlightsID);
            if (!deletedFlight)
            {
                Message = "¡ALERT! *** Vuelo eliminado con éxito";
                ShowFlights();
            }
            else
            {
                Message = "¡ALERT! *** Error, por favor intente de nuevo";
            }
        }

        //Valida que los campos esten diligenciados antes de permitir la creación
        private bool CanExecuteScheduleFlight(object obj)
        {
            bool validForm;
            if (string.IsNullOrWhiteSpace(TimeDeparture)||TimeDeparture.Length <16 || string.IsNullOrWhiteSpace(TimeArrival) || TimeArrival.Length<16 || string.IsNullOrWhiteSpace(FlightNumber))
            {
                validForm = false;
            }
            else {
                validForm = true;

            }

            return validForm;
        }

        //Ejecuta la acción de programar un nuevo vuelo
        private void ExecuteScheduleFlight(object obj)
        {
            NewFlight = new NewFlightModel();

            NewFlight.CityOrigin = SelectedCity.CityID;
            NewFlight.CityDestination = SelectedCityDestinarion.CityID;
            NewFlight.TimeDeparture = TimeDeparture;
            NewFlight.TimeArrival = TimeArrival;
            NewFlight.FlightNumbre = FlightNumber;
            NewFlight.Airline = SelectedAirline.AirlineID;
            NewFlight.FlightStatus = SelectedFlightStatus.StateID;

            var registerFlight = _outboundFlightsRepository.AddFlight(NewFlight);
            if (!registerFlight)
            {
                Message = "¡ALERT! *** Vuelo programado con éxito";
                ShowFlights();
            }
            else
            {
                Message = "¡ALERT! *** Error, por favor intente de nuevo";
            }
        }


        //Se muestra la lista de todos los vuelos programados
        private void ShowFlights()
        {
            _outboundFlightsRepository = new OutboundFlightsRepository();
            var getFlights = _outboundFlightsRepository.GetByAll();

            var lista = (from flight in getFlights
                         select new OutboundFlightsModel
                         {
                             FlightsID = flight.FlightsID,
                             CityOrigin = flight.CityOrigin,
                             CityDestination = flight.CityDestination,
                             TimeDeparture = flight.TimeDeparture,
                             TimeArrival = flight.TimeArrival,
                             FlightNumbre = flight.FlightNumbre,
                             Airline = flight.Airline,
                             FlightStatus = flight.FlightStatus
                         }).ToList();

            OutboundFlights = lista;
        }
        
        //Se obtiene la lista de todas las ciudades
        private void GetAllCities()
        {
            _cityRepository = new CityRepository();
            var getCities= _cityRepository.GetByAll();

            var cities = (from city in getCities
                          select new CityModel
                         {
                              CityID = city.CityID,
                              CityName = city.CityName
                          }).ToList();

            City = cities;
            
        }

        //Se obtiene la lista de todas las Aerolineas
        private void GetAllAirlines()
        {
            _airlineRepository = new AirlieneRepository();
            var getAirlines = _airlineRepository.GetByAll();

            var airlines = (from airline in getAirlines
                          select new AirlineModel
                          {
                              AirlineID = airline.AirlineID,
                              AirlineName = airline.AirlineName
                          }).ToList();

            Airline = airlines;

        }

        //Se obtiene la lista de los estados de vuelo
        private void GetAllStatus()
        {
            _flightStatusRepository = new FlightStatusRepository();
            var getStatus = _flightStatusRepository.GetByAll();

            var status = (from state in getStatus
                          select new FlightStatusModel
                            {
                                StateID = state.StateID,
                              StateName = state.StateName
                            }).ToList();

            FlightStatus = status;

        }

        

    }
}

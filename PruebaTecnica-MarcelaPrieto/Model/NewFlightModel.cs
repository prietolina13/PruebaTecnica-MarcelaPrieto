using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_MarcelaPrieto.Model
{
    //Esta entidad será usada para la creación de un nuevo vuelo
    public class NewFlightModel
    {
        public int CityOrigin { get; set; }
        public int CityDestination { get; set; }
        public string TimeDeparture { get; set; }
        public string TimeArrival { get; set; }
        public string FlightNumbre {  get; set; }
        public int Airline {  get; set; }
        public int FlightStatus { get; set; }


    }
}

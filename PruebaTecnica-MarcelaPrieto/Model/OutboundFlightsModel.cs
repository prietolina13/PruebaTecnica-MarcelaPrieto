using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_MarcelaPrieto.Model
{
    public  class OutboundFlightsModel
    {
        public int FlightsID { get; set; }
        public string CityOrigin {  get; set; }
        public string CityDestination { get; set; }
        public string TimeDeparture { get; set;}
        public string TimeArrival { get; set; }
        public string FlightNumbre { get; set;}
        public string Airline {  get; set; }
        public string FlightStatus { get; set; }
    }
}

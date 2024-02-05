using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_MarcelaPrieto.Model
{
    public interface IFlightStatusRepository
    {
        List<FlightStatusModel> GetByAll();
    }
}

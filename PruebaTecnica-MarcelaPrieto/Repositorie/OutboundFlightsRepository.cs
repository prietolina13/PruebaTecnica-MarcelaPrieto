using PruebaTecnica_MarcelaPrieto.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace PruebaTecnica_MarcelaPrieto.Repositorie
{
    public class OutboundFlightsRepository : RepositoryBase, IOutboundFlightsRepository
    {
        public bool AddFlight(NewFlightModel newFlight)
        {
            bool scheduledFlight;

            using (var connection = GetConnectionString())
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO [PruebaTecnica_MarcelaPrieto].[dbo].[Flights] (CityOrigin,CityDestination,TimeDeparture,TimeArrival,FlightNumbre,Airline,FlightStatus)VALUES  (@cityOrigin,@cityDestination,@timeDeparture,@timeArrival,@flightNumbre,@airline,@flightStatus);";
                    command.Parameters.Add("@cityOrigin", SqlDbType.VarChar).Value = newFlight.CityOrigin;
                    command.Parameters.Add("@cityDestination", SqlDbType.VarChar).Value = newFlight.CityDestination;
                    command.Parameters.Add("@timeDeparture", SqlDbType.VarChar).Value = newFlight.TimeDeparture;
                    command.Parameters.Add("@timeArrival", SqlDbType.VarChar).Value = newFlight.TimeArrival;
                    command.Parameters.Add("@flightNumbre", SqlDbType.VarChar).Value = newFlight.FlightNumbre;
                    command.Parameters.Add("@airline", SqlDbType.VarChar).Value = newFlight.Airline;
                    command.Parameters.Add("@flightStatus", SqlDbType.VarChar).Value = newFlight.FlightStatus;

                    scheduledFlight = command.ExecuteScalar() == null ? false : true;
                    return scheduledFlight;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al invocar AddFlight en OutboundFlightsRepository: {ex.Message}");
                    return false;
                }
            }
        }       

        public List<OutboundFlightsModel> GetByAll()
        {
            List<OutboundFlightsModel> getFlights = [];
            OutboundFlightsModel flight = null;

            using (var connection = GetConnectionString())
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT  FlightsID,C.CityName AS CityOrigin, C2.CityName AS CityDestination, FORMAT(TimeDeparture, 'dd/MM/yyyy hh:mm') AS TimeDeparture, FORMAT(TimeArrival, 'dd/MM/yyyy hh:mm')AS TimeArrival, FlightNumbre, A.AirlineName AS Airline, S.StateName AS FlightStatus, F.[State] FROM [PruebaTecnica_MarcelaPrieto].[dbo].[Flights]  AS F  INNER JOIN [PruebaTecnica_MarcelaPrieto].[dbo].[City] AS C ON F.CityOrigin = C.CityID INNER JOIN [PruebaTecnica_MarcelaPrieto].[dbo].[City] AS C2 ON F.CityDestination = C2.CityID INNER JOIN [PruebaTecnica_MarcelaPrieto].[dbo].[Airline] AS A ON F.Airline = A.AirlineID INNER JOIN [PruebaTecnica_MarcelaPrieto].[dbo].[State] AS S ON F.FlightStatus = S.StateID WHERE F.[State]= 1";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            flight = new OutboundFlightsModel
                            {
                                FlightsID = reader.GetInt32(0),
                                CityOrigin = reader[1].ToString(),
                                CityDestination = reader[2].ToString(),
                                TimeDeparture = reader[3].ToString(),
                                TimeArrival = reader[4].ToString(),
                                FlightNumbre = reader[5].ToString(),
                                Airline = reader[6].ToString(),
                                FlightStatus = reader[7].ToString()
                                
                            };

                            getFlights.Add(flight);
                        }
                    }
                    return getFlights;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al invocar GetByAll en OutboundFlightsRepository: {ex.Message}");
                    return getFlights;
                }                
            }
        }

        public bool LogicalDelete(int idFlight)
        {
            bool eliminatedFlight;

            using (var connection = GetConnectionString())
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "UPDATE [PruebaTecnica_MarcelaPrieto].[dbo].[Flights] SET [State] = 0 WHERE FlightsID = @flightId";
                    command.Parameters.Add("@flightId", SqlDbType.VarChar).Value = idFlight;

                    eliminatedFlight = command.ExecuteScalar() == null ? false : true;
                    return eliminatedFlight;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al invocar LogicalDelete en OutboundFlightsRepository: {ex.Message}");
                    return false;
                }
            }
        }
    }
}

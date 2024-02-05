using PruebaTecnica_MarcelaPrieto.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_MarcelaPrieto.Repositorie
{
    public class AirlieneRepository : RepositoryBase, IAirlineRepository
    {
        public List<AirlineModel> GetByAll()
        {
            List<AirlineModel> getAirlines = [];
            AirlineModel airline = null;

            using (var connection = GetConnectionString())
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM [PruebaTecnica_MarcelaPrieto].[dbo].[Airline] WHERE [State] = 1;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            airline = new AirlineModel
                            {
                                AirlineID = reader.GetInt32(0),
                                AirlineName = reader[1].ToString()
                            };

                            getAirlines.Add(airline);
                        }
                    }
                    return getAirlines;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al invocar GetByAll en AirlieneRepository: {ex.Message}");
                    return getAirlines;
                }
            }
        }
    }
}

using PruebaTecnica_MarcelaPrieto.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_MarcelaPrieto.Repositorie
{
    public class FlightStatusRepository : RepositoryBase, IFlightStatusRepository
    {
        public List<FlightStatusModel> GetByAll()
        {
            List<FlightStatusModel> getStatus = [];
            FlightStatusModel status = null;

            using (var connection = GetConnectionString())
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM [PruebaTecnica_MarcelaPrieto].[dbo].[State] WHERE [State] = 1;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            status = new FlightStatusModel
                            {
                                StateID = reader.GetInt32(0),
                                StateName = reader[1].ToString()
                            };

                            getStatus.Add(status);
                        }
                    }
                    return getStatus;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al invocar GetByAll en FlightStatusRepository: {ex.Message}");
                    return getStatus;
                }
            }
        }
    }
}

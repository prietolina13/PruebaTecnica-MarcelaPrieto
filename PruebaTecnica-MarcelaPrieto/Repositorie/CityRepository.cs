using PruebaTecnica_MarcelaPrieto.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace PruebaTecnica_MarcelaPrieto.Repositorie
{
    public class CityRepository : RepositoryBase, ICityRepository
    {
        public List<CityModel> GetByAll()
        {
            List<CityModel> getCities = [];
            CityModel city = null;

            using (var connection = GetConnectionString())
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM [PruebaTecnica_MarcelaPrieto].[dbo].[City] WHERE [State] = 1;";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            city = new CityModel
                            {
                                CityID = reader.GetInt32(0),
                                CityName = reader[1].ToString()
                            };

                            getCities.Add(city);
                        }
                    }
                    return getCities;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al invocar GetByAll en CityRepository: {ex.Message}");
                    return getCities;
                }
            }
        }
    }
}

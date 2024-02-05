using PruebaTecnica_MarcelaPrieto.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_MarcelaPrieto.Repositorie
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public bool Authentication(NetworkCredential credential)
        {
            bool correctUser;

            using (var connection = GetConnectionString())
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM [PruebaTecnica_MarcelaPrieto].[dbo].[User] WHERE UserName=@userName AND [Password]=@password";
                    command.Parameters.Add("@username", SqlDbType.VarChar).Value = credential.UserName;
                    command.Parameters.Add("@password", SqlDbType.VarChar).Value = credential.Password;

                    correctUser = command.ExecuteScalar() == null ? false : true;
                    return correctUser;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error al invocar Authentication en UserRepository: {e.Message}");
                    return false;
                }
            }            
        }
    
        public UserModel GetByUser(string username)
        {
            UserModel user = null;

            using (var connection = GetConnectionString())
            using (var command = new SqlCommand())
            {
                try
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM [PruebaTecnica_MarcelaPrieto].[dbo].[User] WHERE UserName=@userName AND State = 1";
                    command.Parameters.Add("@username", SqlDbType.VarChar).Value = username;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserModel()
                            {
                                UserID = reader.GetInt32(0),
                                UserName = reader[1].ToString(),
                                Password = string.Empty,
                                RoleID= reader.GetInt32(3)
                            };
                        }
                    }
                        return user;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error al invocar GetByUser en UserRepository: {e.Message}");
                    return user;
                }
            }
        }
    }
}

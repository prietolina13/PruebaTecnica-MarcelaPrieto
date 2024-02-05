using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PruebaTecnica_MarcelaPrieto.Repositorie
{
    public abstract class RepositoryBase
    {
        //Cadena de conexión
        private readonly string _connectionString;

        public RepositoryBase()
        {
            _connectionString = "Server=LINAMARCELAP13\\SQLEXPRESS;Database=PruebaTecnica_MarcelaPrieto;Integrated Security=true";
        }

        protected SqlConnection GetConnectionString()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

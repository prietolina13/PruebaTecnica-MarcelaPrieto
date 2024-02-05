using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_MarcelaPrieto.Model
{
    public interface IUserRepository
    {
        bool Authentication(NetworkCredential credential);
        UserModel GetByUser(string username);

    }
}

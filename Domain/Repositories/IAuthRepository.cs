using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<int> Login(string username, string password);
    }
}

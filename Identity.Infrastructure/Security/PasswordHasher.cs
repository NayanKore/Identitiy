using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Features.Common.Interfaces;
using BCrypt ;


namespace Identity.Infrastructure.Security
{
    //public class PasswordHasher : IPasswordHasher
    //{
    //    public string Hash(string password) => BCrypt.HashPassword(password);
    //    public bool Verify(string password, string hash) => BCrypt.Verify(password, hash);
    //}
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password);

        public bool Verify(string password, string hash) =>
            BCrypt.Net.BCrypt.Verify(password, hash);

        //public bool Verify(string password, string hash) 
        //{
        //    hash = BCrypt.Net.BCrypt.HashPassword(password);
        //    bool result = BCrypt.Net.BCrypt.Verify(password, hash);
        //    return result;
        //}

    }
}

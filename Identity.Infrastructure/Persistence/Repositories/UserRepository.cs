using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Persistence.AppDbContext;

namespace Identity.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
            => await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        //   public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        //   {
        //       // var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        //       //var query =  _context.Users.Where(u => u.Email == email);
        //       ////var query =  _context.Users.FirstOrDefault();

        //       //return (User)query;
        //       ////return query;
        //       ///
        //       // var canConnect = await _context.Database.CanConnectAsync();

        //       //if (canConnect)
        //       //    Console.WriteLine("✅ DB Connected");
        //       //else
        //       //    Console.WriteLine("❌ DB Not Connected");
        //       try
        //       {
        //           await _context.Database.OpenConnectionAsync();
        //           Console.WriteLine("✅ DB Connected");
        //       }
        //       catch (Exception ex)
        //       {
        //           Console.WriteLine("❌ ERROR: " + ex.Message);
        //           Console.WriteLine("❌ INNER: " + ex.InnerException?.Message);
        //       }
        //       return await _context.Users
        //.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        //   }

        public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
            => await _context.Users.FindAsync(new object[] { id }, cancellationToken);

public async Task AddAsync(User user, CancellationToken cancellationToken)
{
    await _context.Users.AddAsync(user, cancellationToken);
    await _context.SaveChangesAsync(cancellationToken);
}

public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    => await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);

public async Task<bool> ExistsByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken)
    => await _context.Users.AnyAsync(u => u.EmployeeId == employeeId, cancellationToken);
    }
}

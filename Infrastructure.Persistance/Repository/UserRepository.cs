using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> ExistEmail(string email)
        {
            return await _db.Users.AnyAsync(x => x.Email == email);
        }
    }
}

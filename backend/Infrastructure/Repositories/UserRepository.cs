using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context) {}

        public async Task<UserEntity> GetUserByEmail(string email)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}

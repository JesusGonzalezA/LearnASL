using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
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

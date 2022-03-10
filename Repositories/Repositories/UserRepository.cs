using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCore.Entities.Entities;
using EfCore.Migrations;
using EfCore.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EfCore.Repositories.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EfCoreContext _efCoreContext;
        private readonly DbSet<User> _userDbSet;

        public UserRepository(EfCoreContext efCoreContext)
        {
            _efCoreContext = efCoreContext;
            _userDbSet = efCoreContext.Set<User>();
        }

        public Task AddUser(User user)
        {
            throw new NotImplementedException();
        }


        public Task DeleteUser(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserAsync(long id, bool asNoTracking = false)
        {
            var user = await _userDbSet
                .AsTracking(asNoTracking ? QueryTrackingBehavior.NoTracking : QueryTrackingBehavior.TrackAll)
                .FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<User> GetUserByCredentialsAsync(string login, string password, bool asNoTracking = true)
        {
            var user = await _userDbSet
                .AsTracking(asNoTracking ? QueryTrackingBehavior.NoTracking : QueryTrackingBehavior.TrackAll)
                .FirstOrDefaultAsync(x => x.Login == login && x.Password == password);
            return user;
        }

        public Task UpdateUser(long id, User user)
        {
            throw new NotImplementedException();
        }
    }
}

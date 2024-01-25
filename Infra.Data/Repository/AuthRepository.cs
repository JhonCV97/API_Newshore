using Domain.Interfaces;
using Domain.Models.User;
using Infra.Data.Context;

namespace Infra.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private AplicationDBContext _ctx;

        public AuthRepository(AplicationDBContext ctx)
        {
            _ctx = ctx;
        }

        public bool ValidateByLogin(string login)
        {
            try
            {
                var user = _ctx.Users.Where(c => c.Login == login).FirstOrDefault();
                return user == null ? false : true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public IQueryable<User> GetUsers()
        {
            return _ctx.Users;
        }
    }
}

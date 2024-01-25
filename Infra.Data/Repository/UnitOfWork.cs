using Domain.Interfaces;
using Domain.Models.Flight;
using Domain.Models.Journey;
using Domain.Models.JourneyFlight;
using Domain.Models.Role;
using Domain.Models.Transport;
using Domain.Models.User;
using Infra.Data.Context;

namespace Infra.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AplicationDBContext _ctx;
        public IRepository<Role> RoleRepository => new BaseRepository<Role>(_ctx);
        public IRepository<User> UserRepository => new BaseRepository<User>(_ctx);
        public IRepository<Transport> TransportRepository => new BaseRepository<Transport>(_ctx);
        public IRepository<Flight> FlightRepository => new BaseRepository<Flight>(_ctx);
        public IRepository<Journey> JourneyRepository => new BaseRepository<Journey>(_ctx);
        public IRepository<JourneyFlight> JourneyFlightRepository => new BaseRepository<JourneyFlight>(_ctx);

        public UnitOfWork(AplicationDBContext ctx)
        {
            _ctx = ctx;

        }

        public void Dispose()
        {
            if (_ctx != null)
            {
                _ctx.Dispose();
            }
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}

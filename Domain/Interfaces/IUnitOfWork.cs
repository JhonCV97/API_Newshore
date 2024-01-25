using Domain.Models.Flight;
using Domain.Models.Journey;
using Domain.Models.JourneyFlight;
using Domain.Models.Role;
using Domain.Models.Transport;
using Domain.Models.User;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Role> RoleRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Transport> TransportRepository { get; }
        IRepository<Flight> FlightRepository { get; }
        IRepository<Journey> JourneyRepository { get; }
        IRepository<JourneyFlight> JourneyFlightRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}

using AHuiHou.Domain.Entities;

namespace AHuiHou.Domain.Interfaces;

public interface IReservationRepository : IRepository<Reservation>
{
    Task<bool> HasConflictingReservation(int tableId, DateTime startTime, DateTime endTime, CancellationToken cancellationToken = default);
    Task<IEnumerable<Reservation>> GetUserReservations(Guid userId, CancellationToken cancellationToken = default);
}


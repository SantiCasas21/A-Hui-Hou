using AHuiHou.Domain.Entities;
using AHuiHou.Domain.Interfaces;
using AHuiHou.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AHuiHou.Infrastructure.Repositories;

public class ReservationRepository : Repository<Reservation>, IReservationRepository
{
    public ReservationRepository(AHuiHouDbContext context) : base(context) { }

    public async Task<bool> HasConflictingReservation(int tableId, DateTime startTime, DateTime endTime, CancellationToken cancellationToken = default)
    {
        return await _context.Reservations.AnyAsync(r =>
            r.TableId == tableId &&
            (r.Status == "Pending" || r.Status == "Confirmed") &&
            r.StartTime < endTime &&
            r.EndTime > startTime,
            cancellationToken);
    }

    public async Task<IEnumerable<Reservation>> GetUserReservations(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Reservations
            .Where(r => r.UserId == userId)
            .Include(r => r.Table)
                .ThenInclude(t => t.Area)
            .OrderByDescending(r => r.StartTime)
            .ToListAsync(cancellationToken);
    }
}


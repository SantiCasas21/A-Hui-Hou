using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Requests;
using AHuiHou.Application.DTOs.Responses;

namespace AHuiHou.Application.Interfaces;

public interface IReservationService
{
    Task<Result<ReservationResponse>> CreateReservationAsync(Guid userId, CreateReservationRequest request, CancellationToken cancellationToken = default);
    Task<Result<Unit>> CancelReservationAsync(Guid reservationId, Guid userId, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ReservationResponse>>> GetUserReservationsAsync(Guid userId, CancellationToken cancellationToken = default);
}


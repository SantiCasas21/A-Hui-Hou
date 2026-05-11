using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Requests;
using AHuiHou.Application.DTOs.Responses;
using AHuiHou.Application.Interfaces;
using AHuiHou.Domain.Entities;
using AHuiHou.Domain.Enums;
using AHuiHou.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AHuiHou.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ReservationService> _logger;

    public ReservationService(IUnitOfWork unitOfWork, ILogger<ReservationService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<ReservationResponse>> CreateReservationAsync(Guid userId, CreateReservationRequest request, CancellationToken cancellationToken = default)
    {
        var hasConflict = await _unitOfWork.Reservations.HasConflictingReservation(
            request.TableId, request.StartTime, request.EndTime, cancellationToken);

        if (hasConflict)
            return Result<ReservationResponse>.Failure(
                "The selected table is already reserved for this time slot",
                "RESERVATION_CONFLICT");

        var table = await _unitOfWork.Tables.GetByIdAsync(request.TableId, cancellationToken);
        if (table is null)
            return Result<ReservationResponse>.Failure("Table not found", "TABLE_NOT_FOUND");

        var reservation = new Reservation
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            TableId = request.TableId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Status = ReservationStatus.Confirmed.ToString()
        };

        await _unitOfWork.Reservations.AddAsync(reservation, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Reservation {Id} created for table {Table} by user {User}",
            reservation.Id, request.TableId, userId);

        return Result<ReservationResponse>.Success(new ReservationResponse(
            reservation.Id,
            table.Id,
            table.TableNumber,
            table.Area?.Name ?? "",
            reservation.StartTime,
            reservation.EndTime,
            reservation.Status));
    }

    public async Task<Result<Unit>> CancelReservationAsync(Guid reservationId, Guid userId, CancellationToken cancellationToken = default)
    {
        var reservation = await _unitOfWork.Reservations.GetByIdAsync(reservationId, cancellationToken);
        if (reservation is null)
            return Result<Unit>.Failure("Reservation not found", "RESERVATION_NOT_FOUND");

        if (reservation.UserId != userId)
            return Result<Unit>.Failure("Not authorized to cancel this reservation", "UNAUTHORIZED");

        if (reservation.Status == ReservationStatus.Cancelled.ToString())
            return Result<Unit>.Failure("Reservation is already cancelled", "ALREADY_CANCELLED");

        reservation.Status = ReservationStatus.Cancelled.ToString();
        _unitOfWork.Reservations.Update(reservation);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Reservation {Id} cancelled", reservationId);
        return Result<Unit>.Success(new Unit());
    }

    public async Task<Result<IEnumerable<ReservationResponse>>> GetUserReservationsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var reservations = await _unitOfWork.Reservations.GetUserReservations(userId, cancellationToken);
        var response = reservations.Select(r => new ReservationResponse(
            r.Id, r.TableId, r.Table?.TableNumber ?? "", r.Table?.Area?.Name ?? "",
            r.StartTime, r.EndTime, r.Status));

        return Result<IEnumerable<ReservationResponse>>.Success(response);
    }
}


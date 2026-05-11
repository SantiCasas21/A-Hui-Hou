using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Requests;
using AHuiHou.Application.DTOs.Responses;
using AHuiHou.Application.Interfaces;
using AHuiHou.Domain.Entities;
using AHuiHou.Domain.Interfaces;

namespace AHuiHou.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<UserResponse>> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return Result<UserResponse>.Failure("User not found", "USER_NOT_FOUND");

        var wallet = await _unitOfWork.LoyaltyWallets.GetByUserIdAsync(userId, cancellationToken);

        return Result<UserResponse>.Success(new UserResponse(
            user.Id, user.FirstName, user.LastName, user.Email, user.CreatedAt, wallet?.Balance ?? 0));
    }

    public async Task<Result<UserResponse>> AssignMembershipAsync(Guid userId, CreateMembershipRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId, cancellationToken);
        if (user is null)
            return Result<UserResponse>.Failure("User not found", "USER_NOT_FOUND");

        var membership = new UserMembership
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            MembershipTypeId = request.MembershipTypeId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsActive = true
        };

        await _unitOfWork.AddEntityAsync(membership, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var wallet = await _unitOfWork.LoyaltyWallets.GetByUserIdAsync(userId, cancellationToken);

        return Result<UserResponse>.Success(new UserResponse(
            user.Id, user.FirstName, user.LastName, user.Email, user.CreatedAt, wallet?.Balance ?? 0));
    }
}


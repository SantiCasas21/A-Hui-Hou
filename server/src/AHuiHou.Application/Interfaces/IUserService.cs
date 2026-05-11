using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Requests;
using AHuiHou.Application.DTOs.Responses;

namespace AHuiHou.Application.Interfaces;

public interface IUserService
{
    Task<Result<UserResponse>> GetProfileAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Result<UserResponse>> AssignMembershipAsync(Guid userId, CreateMembershipRequest request, CancellationToken cancellationToken = default);
}


namespace AHuiHou.Application.DTOs.Requests;

public record CreateMembershipRequest(
    int MembershipTypeId,
    DateOnly StartDate,
    DateOnly? EndDate
);


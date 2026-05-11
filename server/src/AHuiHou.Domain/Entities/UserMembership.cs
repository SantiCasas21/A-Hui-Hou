namespace AHuiHou.Domain.Entities;

public class UserMembership
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int MembershipTypeId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool IsActive { get; set; } = true;

    public User User { get; set; } = null!;
    public MembershipType MembershipType { get; set; } = null!;
}


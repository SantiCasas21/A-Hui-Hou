using AHuiHou.Domain.Entities;
using AHuiHou.Domain.Interfaces;
using AHuiHou.Infrastructure.Data;

namespace AHuiHou.Infrastructure.Repositories;

public class MembershipTypeRepository : Repository<MembershipType>, IMembershipTypeRepository
{
    public MembershipTypeRepository(AHuiHouDbContext context) : base(context) { }
}


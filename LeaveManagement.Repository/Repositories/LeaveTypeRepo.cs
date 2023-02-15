using LeaveManagement.Data;
using LeaveManagement.Repository.Interfaces;

namespace LeaveManagement.Repository.Repositories;

public class LeaveTypeRepo : GenericRepo<LeaveType>, ILeaveTypeRepo
{
    public LeaveTypeRepo(ApplicationDbContext context) : base(context)
    {

    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using LeaveManagement.Data;
using LeaveManagement.Common.Constants;
using LeaveManagement.Common.Models;
using LeaveManagement.Repository.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace LeaveManagement.Repository.Repositories;

public class LeaveAllocationRepo : GenericRepo<LeaveAllocation>, ILeaveAllocationRepo
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Employee> _userManager;
    private readonly ILeaveTypeRepo _leaveTypeRepo;
    private readonly AutoMapper.IConfigurationProvider _configurationProvider;
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;

    public LeaveAllocationRepo(ApplicationDbContext context,
        UserManager<Employee> userManager,
        ILeaveTypeRepo leaveTypeRepo,
        AutoMapper.IConfigurationProvider configurationProvider,
        IEmailSender emailSender,
        IMapper mapper) : base(context)
    {
        _context = context;
        _userManager = userManager;
        _leaveTypeRepo = leaveTypeRepo;
        _configurationProvider = configurationProvider;
        _emailSender = emailSender;
        _mapper = mapper;
    }

    public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period)
    {
        return await _context.LeaveAllocations
            .AnyAsync(
                q => q.EmployeeId == employeeId && 
                q.LeaveTypeId == leaveTypeId && 
                q.Period == period
            );
    }

    public async Task<EmployeeAllocationViewModel> GetEmployeeAllocations(string employeeId)
    {
        var allocations = await _context.LeaveAllocations
            .Include(q => q.LeaveType)
            .Where(q => q.EmployeeId == employeeId)
            .ProjectTo<LeaveAllocationViewModel>(_configurationProvider)
            .ToListAsync();
        
        var employee = await _userManager.FindByIdAsync(employeeId);

        var employeeAllocationModel = _mapper.Map<EmployeeAllocationViewModel>(employee);
        employeeAllocationModel.LeaveAllocations = allocations;

        return employeeAllocationModel;
    }

    public async Task<LeaveAllocationEditViewModel> GetEmployeeAllocation(int id)
    {
        var allocation = await _context.LeaveAllocations
            .Include(q => q.LeaveType)
            .ProjectTo<LeaveAllocationEditViewModel>(_configurationProvider)
            .FirstOrDefaultAsync(q => q.Id == id);

        var employee = await _userManager.FindByIdAsync(allocation.EmployeeId);

        var model = allocation;
        
        model.Employee = _mapper.Map<EmployeeListViewModel>(await _userManager.FindByIdAsync(allocation.EmployeeId));

        return model;
    }

    /**/
    public async Task LeaveAllocation(int leaveTypeId)
    {
        var employees = await _userManager.GetUsersInRoleAsync(Roles.User);
        var period = DateTime.Now.Year;
        var leaveType = await _leaveTypeRepo.GetAsync(leaveTypeId);
        var allocations = new List<LeaveAllocation>();
        var employeeNewAllocations = new List<Employee>();

        foreach (var employee in employees)
        {
            if (await AllocationExists(employee.Id, leaveTypeId, period))
                continue;

            allocations.Add(new LeaveAllocation
            {
                EmployeeId = employee.Id,
                LeaveTypeId = (int)leaveTypeId,
                Period = period,
                NumberOfDays = leaveType.DefaultDays
            });
            
            employeeNewAllocations.Add(employee);

            await AddRangeAsync(allocations);
        }

        foreach (var employee in employeeNewAllocations)
        {
            if (await AllocationExists(employee.Id, leaveTypeId, period))
                continue;

            await _emailSender.SendEmailAsync(employee.Email,
            $"Leave ALlocation Posted for {period}", $"Your {leaveType.Name} are set to: {leaveType.DefaultDays}");
        }
    }

    public async Task<bool> UpdateEmployeeAllocation(LeaveAllocationEditViewModel model)
    {
        var leaveAllocation = await GetAsync(model.Id);

        if (leaveAllocation == null)
        {
            return false;
        }

        leaveAllocation.Period = model.Period;
        leaveAllocation.NumberOfDays = model.NumberOfDays;
        await UpdateAsync(leaveAllocation);

        return true;
    }

    public async Task<LeaveAllocation?> GetEmployeeAllocation(string employeeId, int leaveTypeId)
    {
        return await _context.LeaveAllocations
            .FirstOrDefaultAsync(
                get => get.EmployeeId == employeeId &&
                get.LeaveTypeId == leaveTypeId
            );
    }
}

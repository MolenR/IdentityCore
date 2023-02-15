using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using LeaveManagement.Common.Constants;
using LeaveManagement.Data;
using LeaveManagement.Common.Models;
using LeaveManagement.Repository.Interfaces;
using AutoMapper;

namespace LeaveManagement.Web.Controllers;

public class EmployeesController : Controller
{
    private readonly UserManager<Employee> userManager;
    private readonly IMapper mapper;
    private readonly ILeaveAllocationRepo leaveAllocationRepo;
    private readonly ILeaveTypeRepo leaveTypeRepo;

    public EmployeesController(UserManager<Employee> userManager,
        IMapper mapper, ILeaveAllocationRepo leaveAllocationRepo, ILeaveTypeRepo leaveTypeRepo)
    {
        this.userManager = userManager;
        this.mapper = mapper;
        this.leaveAllocationRepo = leaveAllocationRepo;
        this.leaveTypeRepo = leaveTypeRepo;
    }

    // GET: EmployeesController
    public async Task<ActionResult> Index()
    {
        var employees = await userManager.GetUsersInRoleAsync(Roles.User);
        var model = mapper.Map<List<EmployeeListViewModel>>(employees);
        return View(model);
    }

    // GET: EmployeesController/ViewAllocations/employeeId
    public async Task<ActionResult> ViewAllocations(string id)
    {
        var model = await leaveAllocationRepo.GetEmployeeAllocations(id);
        return View(model);
    }

    // GET: EmployeesController/EditAllocation/5
    public async Task<ActionResult> EditAllocation(int id)
    {
        var model = await leaveAllocationRepo.GetEmployeeAllocation(id);
        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    // POST: EmployeesController/EditAllocation/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditAllocation(int id, LeaveAllocationEditViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (await leaveAllocationRepo.UpdateEmployeeAllocation(model))
                {
                    return RedirectToAction(nameof(ViewAllocations), new { id = model.EmployeeId });
                }
            }
        }
        catch
        {
            ModelState.AddModelError(string.Empty, "Error Attempt Failed");
        }

        model.Employee = mapper.Map<EmployeeListViewModel>(await userManager.FindByIdAsync(model.EmployeeId));
        model.LeaveType = mapper.Map<LeaveTypeViewModel>(await leaveTypeRepo.GetAsync(model.LeaveTypeId));

        return View(model);
    }
}

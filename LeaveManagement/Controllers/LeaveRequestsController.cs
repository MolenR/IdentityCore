using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using LeaveManagement.Common.Models;
using LeaveManagement.Common.Constants;
using LeaveManagement.Repository.Interfaces;

namespace LeaveManagement.Web.Controllers;

[Authorize]
public class LeaveRequestsController : Controller
{
    private readonly ILeaveTypeRepo leaveTypeRepo;
    private readonly ILeaveRequestRepo leaveRequestRepo;
    private readonly ILogger<LeaveRequestsController> logger;

    public LeaveRequestsController(ILeaveTypeRepo leaveTypeRepo, ILeaveRequestRepo leaveRequestRepo, ILogger<LeaveRequestsController> logger)
    {
        this.leaveTypeRepo = leaveTypeRepo;
        this.leaveRequestRepo = leaveRequestRepo;
        this.logger = logger;
    }

    [Authorize(Roles = Roles.Administrator)]
    // GET: LeaveRequests
    public async Task<IActionResult> Index()
    {
        var model = await leaveRequestRepo.GetAdminLeaveRequestList();
        return View(model);
    }

    public async Task<ActionResult> MyLeave()
    {
        var model = await leaveRequestRepo.GetLeaveDetails();
        return View(model);
    }

    // GET: LeaveRequests/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var model = await leaveRequestRepo.GetLeaveRequestAsync(id);
        if(model == null)
        {
            return NotFound();
        }
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ApproveRequest(int id, bool approved) /* ID = 0 */
    {
        try
        {
            await leaveRequestRepo.ChangeApprovalStatus(id, approved);
        }
        catch (Exception)
        {

            ModelState.AddModelError(string.Empty, "Operation Failed Try Again");
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ActionName("Cancel")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id) 
    {
        try
        {
            await leaveRequestRepo.CancelLeaveRequest(id); 
        }
        catch (Exception ex)
        {

            logger.LogError(ex, "Error Canceling Leave Request");
        }

        return RedirectToAction(nameof(MyLeave));
    }

    // GET: LeaveRequests/Create
    public async Task<IActionResult> Create()
    {
        var model = new LeaveRequestCreateViewModel
        {
            LeaveTypes = new SelectList(await leaveTypeRepo.GetAllAsync(),
                                        "Id",
                                        "Name")
        };
        
        return View(model);
    }

    // POST: LeaveRequests/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveRequestCreateViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isValidRequest = await leaveRequestRepo.CreateLeaveRequest(model);
                if (isValidRequest)
                {
                    return RedirectToAction(nameof(MyLeave));
                }
                ModelState.AddModelError(string.Empty, "Operation Failed Try Again");
            }
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Operation Failed Try Again");
        }

        model.LeaveTypes = new SelectList(await leaveTypeRepo.GetAllAsync(),
                                          "Id",
                                          "Name",
                                          model.LeaveTypeId);
        return View(model);
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using LeaveManagement.Data;
using LeaveManagement.Common.Constants;
using LeaveManagement.Common.Models;
using LeaveManagement.Repository.Interfaces;
using AutoMapper;

namespace LeaveManagement.Web.Controllers;

[Authorize(Roles = Roles.Administrator)]
public class LeaveTypesController : Controller
{
    private readonly ILeaveTypeRepo leaveTypeRepo;
    private readonly IMapper mapper;

    private readonly ILeaveAllocationRepo LeaveAllocationRepo;

    public LeaveTypesController(ILeaveTypeRepo leaveTypeRepo, IMapper mapper, ILeaveAllocationRepo leaveAllocationRepo)
    {
        this.leaveTypeRepo = leaveTypeRepo;
        this.mapper = mapper;
        LeaveAllocationRepo = leaveAllocationRepo;
    }
    
    // GET: LeaveTypes
    public async Task<IActionResult> Index()
    {
        var leaveTypes = mapper.Map<List<LeaveTypeViewModel>>(await leaveTypeRepo.GetAllAsync());
          return leaveTypeRepo.GetAllAsync != null ? 
                      View(leaveTypes) :
                      Problem("Entity set 'ApplicationDbContext.LeaveTypes'  is null.");
    }

    // GET: LeaveTypes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        var leaveType = await leaveTypeRepo.GetAsync(id);
        if (leaveType == null)
        {
            return NotFound();
        }
        var leaveTypeViewModel = mapper.Map<LeaveTypeViewModel>(leaveType);
        return View(leaveTypeViewModel);
    }

    // GET: LeaveTypes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LeaveTypes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LeaveTypeViewModel leaveTypeViewModel)
    {
        if (ModelState.IsValid)
        {
            var leaveType = mapper.Map<LeaveType>(leaveTypeViewModel);
            await leaveTypeRepo.AddAsync(leaveType);
            return RedirectToAction(nameof(Index));
        }
        return View(leaveTypeViewModel);
    }

    // GET: LeaveTypes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        var leaveType = await leaveTypeRepo.GetAsync(id);
        if (leaveType == null)
        {
            return NotFound();
        }
        var leaveTypeViewModel = mapper.Map<LeaveTypeViewModel>(leaveType);
        return View(leaveTypeViewModel);
    }

    // POST: LeaveTypes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, LeaveTypeViewModel leaveTypeViewModel)
    {
        if (id != leaveTypeViewModel.Id)
        {
            return NotFound();
        }

        var leaveType = await leaveTypeRepo.GetAsync(id);
        if(leaveType == null)
        {
            return NotFound();

        }

        if (ModelState.IsValid)
        {
            try
            {
                mapper.Map(leaveTypeViewModel, leaveType);
                await leaveTypeRepo.UpdateAsync(leaveType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await leaveTypeRepo.Exists(leaveTypeViewModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(leaveTypeViewModel);
    }

    // POST: LeaveTypes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await leaveTypeRepo.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AllocateLeave(int id)
    {
        await LeaveAllocationRepo.LeaveAllocation(id);
        return RedirectToAction(nameof(Index));
    }
}

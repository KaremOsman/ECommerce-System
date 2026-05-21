using AdminDashboard.Models;
using Domain.Entities.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Identity;

namespace AdminDashboard.Controllers
{
    public class UsersController(StoreIdentityDbContext _context, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usersWithRoles = await _context.Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    DisplayName = u.DisplayName,
                    Email = u.Email!,
                    PhoneNumber = u.PhoneNumber!,
                    UserName = u.UserName!,
                    Roles = _context.UserRoles
                                    .Where(ur => ur.UserId == u.Id)
                                    .Join(_context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name ?? string.Empty)
                                    .ToList()
                }).ToListAsync();

            return View(usersWithRoles);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _roleManager.Roles.ToArrayAsync();
            var viewModel = new UserRoleViewModel
            {
                UserId = user!.Id,
                UserName = user.UserName!,
                Roles = roles.Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Name = r.Name ?? "No Name",
                    IsSelected = _userManager.IsInRoleAsync(user, r.Name!).Result
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(UserRoleViewModel model, string id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null)
                    return NotFound();
                var userRoles =await _userManager.GetRolesAsync(user);
                foreach (var role in model.Roles)
                {
                    if(userRoles.Any(r => r == role.Name)&& !role.IsSelected)
                    {
                        await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                    if(!userRoles.Any(r => r == role.Name)&& role.IsSelected)
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
    
}

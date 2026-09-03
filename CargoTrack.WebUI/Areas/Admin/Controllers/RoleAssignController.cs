using CargoTrack.DTO.DTOs.UserDtos;
using CargoTrack.Entity.Entities;
using CargoTrack.WebUI.Consts;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CargoTrack.WebUI.Areas.Admin.Controllers
{
    [Area(Area.Admin)]
    [Authorize(Roles = Roles.Admin)]

    public class RoleAssignController(
    UserManager<AppUser> _userManager,
    RoleManager<AppRole> _roleManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var mappedUsers = users.Adapt<List<ResultUserDto>>();

            foreach (var user in mappedUsers)
            {
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id.ToString()));
                user.Roles = roles;
            }

            return View(mappedUsers);
        }

        public async Task<IActionResult> AssignRole(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var userRoles = await _userManager.GetRolesAsync(user);

            var roles = _roleManager.Roles.ToList();

            var roleAssignList = new List<RoleAssignDto>();

            ViewBag.fullName = string.Join(" ", user.FirstName, user.LastName);

            foreach (var role in roles)
            {
                var roleAssignDto = new RoleAssignDto
                {
                    UserId = user.Id,
                    RoleId = role.Id,
                    RoleName = role.Name,
                    RoleExist = userRoles.Contains(role.Name)
                };

                roleAssignList.Add(roleAssignDto);
            }

            return View(roleAssignList);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignDto> roleAssignDtos)
        {
            var userId = roleAssignDtos.FirstOrDefault()?.UserId;
            var user = await _userManager.FindByIdAsync(userId.ToString());
            foreach (var roleAssignDto in roleAssignDtos)
            {
                if (roleAssignDto.RoleExist)
                {
                    await _userManager.AddToRoleAsync(user, roleAssignDto.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, roleAssignDto.RoleName);
                }
            }
            return RedirectToAction("Index");
        }
    }
}


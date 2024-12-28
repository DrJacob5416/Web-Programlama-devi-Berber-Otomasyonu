using Berber.Areas.Admin.Models;
using Berber.Models.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Berber.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Kurulum()
        {
            var roles = new[] { "Admin", "Worker" };

            foreach (var role in roles)
            {
                // Rol daha önce eklenmemişse ekle
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new ApplicationRole() { Name= role });
                }
            }/*
            for (int i = 1; i < 7; i++)
            {
                var user = new ApplicationUser { UserName = $"kullanici{i}@gmail.com", Email = $"kullanici{i}@gmail.com", FullName = $"kullanici{i} " + "soyad" };
                await _userManager.CreateAsync(user, "123456.Aa");
                if(i==1)
                    await _userManager.AddToRoleAsync(user, "Admin");
                if(i==2 || i==3 || i==4)
                    await _userManager.AddToRoleAsync(user, "Worker");
            }*/
            /*
             * // Kullanıcının tüm rollerini alın
    var userRoles = await _userManager.GetRolesAsync(user);

    // Kullanıcıyı her bir rolden çıkar
    var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
             * */
            return RedirectToAction("Index","Users");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.ToList();
            var userList = new List<UserListModel>();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var userModel = new UserListModel()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = userRoles.SingleOrDefault()
                };
                userList.Add(userModel);
            }
            var model = new UserRoleViewModel
            {
                Users = userList,
                Roles = roles
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(string userId, string role)
        {
            // Kullanıcıyı al
            var user = await _userManager.FindByIdAsync(userId);

            // Kullanıcıya ait eski rolleri sil
            var currentRoles = await _userManager.GetRolesAsync(user);
            var resultRemove = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!resultRemove.Succeeded)
            {
                return Json(new { success = false, message = "Roller silinemedi." });
            }

            // Yeni rolü ekle
            var resultAdd = await _userManager.AddToRoleAsync(user, role);

            if (resultAdd.Succeeded)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Rol eklenemedi." });
            }
        }
    }
}

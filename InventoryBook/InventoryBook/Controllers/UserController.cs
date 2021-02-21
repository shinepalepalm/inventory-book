using InventoryBook.BLL.Services.Interfaces;
using InventoryBook.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryBook.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id) => View(_userService.Get(id));

        [HttpPost]
        public IActionResult Edit(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                _userService.Edit(userViewModel);
                return RedirectToAction("Index");
            }

            return View(userViewModel);
        }

        [HttpGet]
        public IActionResult Index() => View(_userService.GetAll());

        [HttpGet]
        public JsonResult CheckLoginForUpdate(int Id, string Login) => Json(!_userService.IsLoginExistsForUpdate(Id, Login));
    }
}

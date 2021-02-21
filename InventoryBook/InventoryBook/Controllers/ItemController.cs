using InventoryBook.BLL.Services.Interfaces;
using InventoryBook.Common.Enums;
using InventoryBook.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryBook.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService) => _itemService = itemService;

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(ItemViewModel itemViewModel)
        {
            if (ModelState.IsValid)
            {
                _itemService.Create(itemViewModel);
                return RedirectToAction("Index");
            }

            return View(itemViewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _itemService.Delete(id);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Details(int id) => View(_itemService.Get(id));

        [HttpGet]
        public IActionResult Edit(int id) => View(_itemService.Get(id));

        [HttpPost]
        public IActionResult Edit(ItemViewModel itemViewModel)
        {
            if (ModelState.IsValid)
            {
                _itemService.Edit(itemViewModel);
                return RedirectToAction("Index");
            }

            return View(itemViewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index() => View(_itemService.GetAll());

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Search(string text, ItemFilter filter)
        {
            if (string.IsNullOrEmpty(text))
            {
                return RedirectToAction("Index");
            }

            return View("Index", _itemService.Filter(text, filter));
        }
    }
}

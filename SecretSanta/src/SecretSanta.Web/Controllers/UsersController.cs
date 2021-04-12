using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;
using SecretSanta.Web.Data;

namespace UserGroup.Web.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index() 
        {
            return View(MockData.Users);
        }

        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserViewModel viewModel)
        {
            MockData.Users.Add(viewModel);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id) {
            MockData.Users[id].Id = id;
            return View(MockData.Users[id]);
        }
        
        [HttpPost]
        public IActionResult Edit(UserViewModel viewModel)
        {
            if (ModelState.IsValid) {
                MockData.Users[viewModel.Id] = viewModel;
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            MockData.Users.RemoveAt(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
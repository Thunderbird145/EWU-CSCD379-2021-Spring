using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SecretSanta.Web.ViewModels;
using SecretSanta.Web.Data;

namespace GroupGroup.Web.Controllers
{
    public class GroupsController : Controller
    {
        public IActionResult Index() 
        {
            return View(MockData.Groups);
        }

        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(GroupViewModel viewModel)
        {
            MockData.Groups.Add(viewModel);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id) {
            MockData.Groups[id].Id = id;
            return View(MockData.Groups[id]);
        }
        
        [HttpPost]
        public IActionResult Edit(GroupViewModel viewModel)
        {
            if (ModelState.IsValid) {
                MockData.Groups[viewModel.Id] = viewModel;
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            MockData.Groups.RemoveAt(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Web.Api;
using SecretSanta.Web.Data;
using SecretSanta.Web.ViewModels;

namespace SecretSanta.Web.Controllers
{
    public class UsersController : Controller
    {
        
        public IUsersClient Client { get; }
        public UsersController(IUsersClient client) {
            Client = (client ?? throw new ArgumentNullException(nameof(client)));
        }

        public async Task<IActionResult> Index()
        {
            ICollection<FullUser> users = await Client.GetAllAsync();
            List<UserViewModel> viewModelUsers = new();
            foreach(FullUser u in users) {
                viewModelUsers.Add(new UserViewModel {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                });
            }
            return View(viewModelUsers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel viewModel)
        {
            FullUser createdUser = new FullUser {
                    Id = viewModel.Id,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                };
            if (ModelState.IsValid)
            {
                await Client.PostAsync(createdUser);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            FullUser fUser = await Client.GetAsync(id);
            UserViewModel viewModel = new UserViewModel {
                Id = fUser.Id,
                FirstName = fUser.FirstName,
                LastName = fUser.LastName,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel viewModel)
        {
            NameUser fUser = new NameUser {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
            };
            if (ModelState.IsValid)
            {
                await Client.PutAsync(viewModel.Id, fUser);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await Client.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {


        private IUserRepository UserManager { get;} = new UserManager();

        public UsersController(IUserRepository userManager) {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // /api/Users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return UserManager.List();
        }

        // /api/Users/<index>
        [HttpGet("{index}")] 
        public User? Get(int index)
        {
            return UserManager.GetItem(index);
        }

        //DELETE /api/Users/<index>
        [HttpDelete("{index}")]
        public ActionResult Delete(int index)
        {
            if (index < 0) {
                return NotFound();
            }
            DeleteMe.Users.RemoveAt(index);
            return Ok();
        }

        // POST /api/Users
        [HttpPost]
        public void Post([FromBody] User user)
        {
            DeleteMe.Users.Add(user);
        }

        //PUT /api/Users/<index>
        [HttpPut("{index}")]
        public void Put(int index, [FromBody] User user) {
            DeleteMe.Users[index] = user;
        }
    }
}
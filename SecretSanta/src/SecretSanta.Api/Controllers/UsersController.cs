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
        [HttpGet("{id}")] 
        public ActionResult<User?> Get(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            User? returnedUser = UserManager.GetItem(id);
            return returnedUser;
        }

        //DELETE /api/Users/<index>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0) {
                return NotFound();
            }
            if (UserManager.Remove(id)) {
                return Ok();
            }
            return NotFound();
        }

        // POST /api/Users
        [HttpPost]
        public ActionResult<User?> Post([FromBody] User? user)
        {
            if (user is null) {
                return BadRequest();
            }
            return UserManager.Create(user);
        }

        //PUT /api/Users/<index>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] User updatedUser) {
            if (updatedUser is null) {
                return BadRequest();
            }
            User? foundUser = UserManager.GetItem(id);
            if (foundUser is not null) {
                if (!string.IsNullOrWhiteSpace(updatedUser.FirstName)) {
                    foundUser.FirstName = updatedUser.FirstName;
                    foundUser.LastName = updatedUser.LastName;
                }
                if (!string.IsNullOrWhiteSpace(updatedUser.LastName)) {
                    foundUser.LastName = updatedUser.LastName;
                }

                UserManager.Save(foundUser);
                return Ok();
            }
            return NotFound();
        }
    }
}
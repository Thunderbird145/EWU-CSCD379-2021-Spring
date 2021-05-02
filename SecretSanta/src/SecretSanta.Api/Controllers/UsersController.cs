using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;
using SecretSanta.Api.DTO;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository Repository { get; }

        public UsersController(IUserRepository repository)
        {
            Repository = repository ?? throw new System.ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        
        public IEnumerable<FullUser> Get()
        {
            List<FullUser> users = new List<FullUser>();
            foreach(User u in Repository.List()) {
                users.Add(new FullUser
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                });
            }
            return users;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<NameUser?> Get(int id)
        {
            User? user = Repository.GetItem(id);
            if (user is null) return NotFound();
            NameUser nameUser = new NameUser();
            nameUser.FirstName = user.FirstName;
            nameUser.LastName = user.LastName;
            return nameUser;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Delete(int id)
        {
            if (Repository.Remove(id))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(FullUser), StatusCodes.Status200OK)]
        public ActionResult<FullUser?> Post([FromBody] FullUser? fUser)
        {
            if (fUser is null)
            {
                return BadRequest();
            }
            User user = new User();
            user.Id = fUser.Id;
            user.FirstName = fUser.FirstName;
            user.LastName = fUser.LastName;
            Repository.Create(user);
            return fUser;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Put(int id, [FromBody] NameUser? updateUser)
        {
            if (updateUser is null)
            {
                return BadRequest();
            }

            User? foundUser = Repository.GetItem(id);
            if (foundUser is not null)
            {
                foundUser.FirstName = updateUser.FirstName ?? "";
                foundUser.LastName = updateUser.LastName ?? "";

                Repository.Save(foundUser);
                return Ok();
            }
            return NotFound();
        }
    }
}

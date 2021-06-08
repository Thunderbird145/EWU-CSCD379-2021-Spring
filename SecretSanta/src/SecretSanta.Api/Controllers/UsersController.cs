﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository UserRepository { get; }

        private IGiftRepository GiftRepository { get; }

        public UsersController(IUserRepository repository, IGiftRepository giftRepository)
        {
            UserRepository = repository ?? throw new System.ArgumentNullException(nameof(repository));
            GiftRepository = giftRepository ?? throw new System.ArgumentNullException(nameof(giftRepository));
        }

        [HttpGet]
        public IEnumerable<Dto.User> Get()
        {
            return UserRepository.List().Select(x => Dto.User.ToDto(x)!);
        }

        [HttpGet("{id}")]
        public ActionResult<Dto.User?> Get(int id)
        {
            Dto.User? user = Dto.User.ToDto(UserRepository.GetItem(id), true);
            if (user is null) return NotFound();
            return user;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult Delete(int id)
        {
            if (UserRepository.Remove(id))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Dto.User), (int)HttpStatusCode.OK)]
        public ActionResult<Dto.User?> Post([FromBody] Dto.User user)
        {
            return Dto.User.ToDto(UserRepository.Create(Dto.User.FromDto(user)!));
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult Put(int id, [FromBody] Dto.UpdateUser? user)
        {
            Data.User? foundUser = UserRepository.GetItem(id);
            if (foundUser is not null)
            {
                foundUser.FirstName = user?.FirstName ?? "";
                foundUser.LastName = user?.LastName ?? "";

                UserRepository.Save(foundUser);
                return Ok();
            }
            return NotFound();
        }
    }
}

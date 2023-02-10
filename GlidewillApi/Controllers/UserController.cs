using GlidewillApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlidewillApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        private readonly ILogger<UserController> _logger;
        private MySQLDBContext _dbContext;
        public UserController(ILogger<UserController> logger, MySQLDBContext context)
        {
            _logger = logger;
            _dbContext = context;
        }


        [HttpGet(Name = "GetUsers")]
        public IList<User> getUsers()
        {

            return this._dbContext.User.Select(x => x).Where(x => x.IsActive).ToList();

        }

        [HttpPost(Name = "AddUser")]
        public async Task<ActionResult<User>> addUser(User user)
        {
            var newUser = new User();
            newUser.IsActive = true;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.created = DateTime.Now.Date;
            this._dbContext.User.Add(newUser);

            await this._dbContext.SaveChangesAsync();

            return await this._dbContext.User.FindAsync(newUser.id);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id, User user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }

            var newUser = new User();
            newUser.id = user.id;
            newUser.FirstName = user.FirstName;
            newUser.LastName = user.LastName;
            newUser.IsActive = true;
           
            return await this.updateUser(newUser);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await this._dbContext.User.FindAsync(id);
            user.IsActive = false;
            return await this.updateUser(user);
        }

        private async Task<NoContentResult> updateUser(User user)
        {
            this._dbContext.Entry(user).State = EntityState.Modified;

            try
            {
                await this._dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }
    }
}

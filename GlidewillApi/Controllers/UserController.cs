using GlidewillApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

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

            return this._dbContext.User.Select(x=> x).Where(x=>x.IsActive).ToList();

        }

        [HttpPost(Name = "AddUser")]
        public async Task<ActionResult<User>> addUser(User user)
        {
            this._dbContext.User.Add(user);

            await this._dbContext.SaveChangesAsync();

           return await this._dbContext.User.FindAsync(user.id);

        }


    }
}

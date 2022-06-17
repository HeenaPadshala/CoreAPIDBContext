using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPIDBContext.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            return Ok(await _dataContext.Users.ToListAsync());
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            var dbUser = await _dataContext.Users.FindAsync(id);
            if (dbUser == null)
                return NotFound("User not found");
            return Ok(dbUser);
        }

        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult<List<UserDTO>>> Add(UserDTO user)
        {
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Users.ToListAsync());
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ActionResult<List<UserDTO>>> Update(UserDTO request)
        {
            var dbUser = await _dataContext.Users.FindAsync(request.Id);
            if (dbUser == null)
                return NotFound("User not found");
            dbUser.UserName = request.UserName;
            dbUser.IsActive = request.IsActive;
            dbUser.Password = request.Password;
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Users.ToListAsync());
        }

        /// <summary>
        /// Delete user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<UserDTO>>> Delete(int id)
        {
            var dbUser = await _dataContext.Users.FindAsync(id);
            if (dbUser == null)
                return NotFound("User not found");
            _dataContext.Users.Remove(dbUser);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Users.ToListAsync());
        }

        /// <summary>
        /// Get user by UserName and Password
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<ActionResult<UserDTO>> GetUserByName(string name, string password)
        {
            var dbUser = _dataContext.Users.Where(p => p.UserName == name && p.Password == password);
            if (dbUser == null)
                return NotFound("User not found");
            return Ok(dbUser);
        }

        /// <summary>
        /// Get all active user
        /// </summary>
        /// <returns></returns>
        [HttpGet("getactive")]
        public async Task<ActionResult<List<UserDTO>>> GetActiveUsers()
        {
            var dbUser = _dataContext.Users.Where(p => p.IsActive == true);
            if (dbUser == null)
                return NotFound("User not found");
            return Ok(dbUser);
        }

        /// <summary>
        /// Get all in-active user
        /// </summary>
        /// <returns></returns>
        [HttpGet("getinactive")]
        public async Task<ActionResult<List<UserDTO>>> GetInActiveUsers()
        {
            var dbUser = _dataContext.Users.Where(p => p.IsActive == false);
            if (dbUser == null)
                return NotFound("User not found");
            return Ok(dbUser);
        }

        ///// <summary>
        ///// Get full name of users
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("getAll")]
        //public async Task<ActionResult<List<UserAPI>>> GetUsers()
        //{
        //    var dbUser = _dataContext.Users.ToListAsync();
        //    if (dbUser == null)
        //        return NotFound("User not found");
        //    UserAPI users = new UserAPI();
        //    Mapper.ReferenceEquals(dbUser, users);
        //    users = dbUser;
        //    foreach (var user in dbUser.Result)
        //    {
        //        user.FullName = user.FirstName + " " + user.LastName;
        //    }
        //    return Ok(dbUser);
        //}
    }
}

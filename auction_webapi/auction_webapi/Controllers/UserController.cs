using auction_webapi.BL;
using auction_webapi.DTO;
using auction_webapi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace auction_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMapper _imapper;
        private IUserService _userService;
        public UserController(IUserService userService, IMapper imapper)
        {
            this._userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this._imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserDTO>>> GetUsers()
        {
            List<User> users = await _userService.GetAsync();
            List<UserDTO> userDTOs = _imapper.Map<List<User>, List<UserDTO>>(users);

            foreach (User user in users)
            {
                IEnumerable<OrderDTO> orderDTOs = _imapper.Map<ICollection<Order>, IEnumerable<OrderDTO>>(user.Orders);
                userDTOs.First(d => d.UserId == user.UserId).Orders = orderDTOs.ToList();
            }

            return userDTOs;
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            User users = await _userService.GetByIdAsync(id);
            UserDTO userDTOs = _imapper.Map<User, UserDTO>(users);
            IEnumerable<OrderDTO> orderDTOs = _imapper.Map<ICollection<Order>, IEnumerable<OrderDTO>>(users.Orders);
            userDTOs.Orders = orderDTOs.ToList();
            return userDTOs;
        }


        [HttpDelete("deleteUser/{id}")]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            return await _userService.DeleteByIdAsync(id);
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> PostUser([FromBody] UserDTO user)
        {
            try
            {
                User u = _imapper.Map<UserDTO, User>(user);
                return await _userService.PostAsync(u);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("updateUser")]
        public async Task<ActionResult<string>> UpdateUser([FromBody] UserDTO user)
        {
            try
            {
                User u = _imapper.Map<UserDTO, User>(user);
                return await _userService.PutAsync(u);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> Login(UserLoginDTO model)
        {
            
                User user = await _userService.GetUserByUsernameAndPassword(model.Email, model.Password);
                if (user == null)
                {
                    return BadRequest("register");
                }
                string token = TokenGenerator.GenerateToken(user.Email, user.Role,user.UserId);
            return new { token };
        }

        

    }
}

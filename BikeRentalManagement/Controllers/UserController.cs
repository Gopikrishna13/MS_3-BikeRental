using BikeRentalManagement.DTOs.RequestDTOs;
using BikeRentalManagement.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
                  _userService=userService;
        }

        [HttpPost("CreateUser")]
        public async Task <IActionResult> CreateUser(UserRequestDTO userRequestDTO)
        {
            try{
                var data=await _userService.CreateUser(userRequestDTO);
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("AllUsers")]
        public async Task<IActionResult>AllUsers()
        {
            try{

                var data=await _userService.AllUsers();
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("UserById")]
        public async Task <IActionResult>UserById(int Id)
        {
            try{
                var data=await _userService.UserById(Id);
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteById")]
        public async Task <IActionResult> DeleteById(int Id)
        {
            try{
                var data=await _userService.DeleteById(Id);
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateUser")]
        public async Task <IActionResult> UpdateUser(int Id,UserRequestDTO userRequestDTO)
        {
            try{
                var data=await _userService.UpdateUser(Id,userRequestDTO);
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("Login")]
        public async Task <IActionResult> Login(LoginRequestDTO loginrequest)
        {
            try{
                var data=await _userService.Login(loginrequest);
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

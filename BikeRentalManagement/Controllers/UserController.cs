using BikeRentalManagement.Database.Entities;
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
public async Task<IActionResult> CreateUser([FromForm] UserRequestDTO userRequestDTO)
{
    try
    {
        

       
        var result = await _userService.CreateUser(userRequestDTO);

      

        return Ok(new { Success = result });
    }
    catch (Exception ex)
    {
       
       
        return BadRequest(new { Success = false, Error = ex.Message });
    }
}

        [HttpPut("UserRequest{id}")]
        public async Task<IActionResult>UserRequest(int id,int status)
        {
            try{
                var data=await _userService.UserRequest(id,status);
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

        [HttpGet("AllUsers")]
        public async Task<IActionResult>AllUsers(int pagenumber,int pagesize)
        {
            try{

                var data=await _userService.AllUsers(pagenumber,pagesize);
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
        public async Task <IActionResult> UpdateUser(int Id,UserUpdateRequestDTO userupdate)
        {
            try{
                var data=await _userService.UpdateUser(Id,userupdate);
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


        [HttpPost("AddEmail")]
        public async Task <IActionResult> AddEmail(Email mail)
        {
            try{
                var data=await _userService.AddEmail(mail);
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

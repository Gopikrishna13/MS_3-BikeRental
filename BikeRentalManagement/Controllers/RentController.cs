using BikeRentalManagement.DTOs.RequestDTOs;
using BikeRentalManagement.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly IRentService _rentservice;

        public RentController(IRentService rentservice)
        {
            _rentservice=rentservice;
        }

        [HttpPost("RequestRent")]
        public async Task<IActionResult>RequestRent(RentRequestDTO rentRequestDTO)
        {
            try{
                var data=await _rentservice.RequestRent(rentRequestDTO);
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("BikeBookedDates")]
        public async Task<IActionResult>GetBikeBookedDates(string RegistrationNumber)
        {
            try{
                var data=await _rentservice.GetBikeBookedDates(RegistrationNumber);
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("AllRequest")]
        public async Task <IActionResult>AllRequest()
        {
            try{
                var data=await _rentservice.AllRequest();
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("RequestById{id}")]
        public async Task <IActionResult>GetRequestById(int id)
        {
            try{
                var data=await _rentservice.GetRequestById(id);
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AcceptRejectRequest{id}")]
        public async Task<IActionResult>AcceptRejectRequest(int id,int status)
        {
           try{
            var data=await _rentservice.AcceptRejectRequest(id,status);
            return Ok(data);

           }catch(Exception ex)
           {
            return BadRequest(ex.Message);
           }
        }

        [HttpPut("CancelRequest{id}")]
        public async Task<IActionResult>CancelRequest(int id)
        {
            try{
                var data=await _rentservice.CancelRequest(id);
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

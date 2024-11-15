using BikeRentalManagement.DTOs.RequestDTOs;
using BikeRentalManagement.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeRentalManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private readonly IBikeService _bikeservice;

        public BikeController(IBikeService bikeService)
        {
            _bikeservice=bikeService;
        }

        [HttpPost("AddBrand")]
        public async Task <IActionResult>AddBrand(BrandRequestDTO brandRequestDTO)
        {
            try{
                var data=await _bikeservice.AddBrand(brandRequestDTO);
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("AllBrands")]
        public async Task <IActionResult>AllBrands()
        {
           try{
            var data=await _bikeservice.AllBrands();
            return Ok(data);

           } catch(Exception ex)
           {
            return BadRequest(ex.Message);
           }
        }

        [HttpPost("AddModel")]
        public async Task <IActionResult> AddModel(ModelRequestDTO modelRequestDTO)
        {
            try{
                var data=await _bikeservice.AddModel(modelRequestDTO);
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetAllModels")]
        public async Task <IActionResult> GetAllModels()
        {
            try{
                var data=await _bikeservice.GetAllModels();
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddBike")]
        public async Task <IActionResult>AddBike(BikeRequestDTO bikeRequestDTO)
        {
            try{
                var data=await _bikeservice.AddBike(bikeRequestDTO);
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("AllBikes")]
        public async Task<IActionResult>AllBikes(int pagenumber,int pagesize)
        {
            try{
                var data=await _bikeservice.AllBikes(pagenumber,pagesize);
                return Ok(data);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

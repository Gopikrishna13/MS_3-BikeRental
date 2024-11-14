using System;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.RequestDTOs;
using BikeRentalManagement.IRepository;
using BikeRentalManagement.IService;

namespace BikeRentalManagement.Service;

public class BikeService:IBikeService
{
    private readonly IBikeRepository _bikerepository;

    public BikeService(IBikeRepository bikeRepository)
    {
        _bikerepository=bikeRepository;
    }
    public async Task<bool>AddBrand(BrandRequestDTO brandRequestDTO)
    {
        var brand=new Brand{
            BrandName=brandRequestDTO.BrandName

        };

        var data=await _bikerepository.AddBrand(brand);
        if(data)
        {
            return true;
        }else{
            return false;
        }

    }

    public async Task <List<Brand>>AllBrands()
    {
        var data=await _bikerepository.AllBrands();

        if(data == null)
        {
            throw new Exception("No Bikes Found!");
        }else{
            return data;
        }
    }

 public async Task <bool> AddModel(ModelRequestDTO modelRequestDTO)
 {
    var model=new Model
    {
        ModelName=modelRequestDTO.ModelName,
        BrandId=modelRequestDTO.BrandId

    };

    var data=await _bikerepository.AddModel(model);

    if(data)
    {
        return true;
    }else{
        return false;
    }
 }

 public async  Task <List<Brand>> GetAllModels()
 {
    var data=await _bikerepository.GetAllModels();

    if(data != null)
    {
        return data;
    }else{
        throw new Exception("No Data Found!");
    }
 }
}

using System;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.RequestDTOs;
using BikeRentalManagement.IRepository;
using BikeRentalManagement.IService;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Org.BouncyCastle.Bcpg.Attr;
using System.Collections.Generic;


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

public async Task<bool> AddBike(BikeRequestDTO bikeRequestDTO)
{
  
    foreach (var bikeUnit in bikeRequestDTO.BikeUnits)
    {
        var chkReg = await _bikerepository.CheckRegNo(bikeUnit.RegistrationNumber);
        if (chkReg == false)
        {
            throw new Exception("Registration Number Already Exists!");
        }
    }

   
    var modelId = await _bikerepository.FindModelId(bikeRequestDTO.ModelName);
  
   var getbikeid = await _bikerepository.AddModelBike(modelId);
   // var getbikeid = await _bikerepository.getbikeId();
   
    var bikeUnits = new List<BikeUnit>();

    foreach (var bikeUnt in bikeRequestDTO.BikeUnits)
    {
      
        var unit = new BikeUnit
        {
            BikeID = getbikeid,
            RegistrationNumber = bikeUnt.RegistrationNumber,
            Year = bikeUnt.Year,
            RentPerDay = bikeUnt.RentPerDay
        };
      
        bikeUnits.Add(unit);

    
        await _bikerepository.AddBikeUnit(unit);


        var bikeImages = new List<BikeImages>();

       
        foreach (var bikeImage in bikeUnt.bikeImages)
        {
            var image = new BikeImages
            {
                UnitId = unit.UnitId, 
                Image = bikeImage.Image
            };
            bikeImages.Add(image);
        }

       
        await _bikerepository.AddBikeImages(bikeImages);
    }

    return true;
}

public async Task<List<Bike>>AllBikes()
{
    var data=await _bikerepository.AllBikes();

    if(data == null)
    {
        throw new Exception("Data Not Found!");
        
    }
    return data;
}




 
 }


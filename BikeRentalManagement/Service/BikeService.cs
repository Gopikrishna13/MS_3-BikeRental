using System;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.RequestDTOs;
using BikeRentalManagement.IRepository;
using BikeRentalManagement.IService;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Org.BouncyCastle.Bcpg.Attr;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BikeRentalManagement.Migrations;
using BikeRentalManagement.DTOs;
using System.IO;


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

public async Task<bool> AddBike([FromForm] BikeRequestDTO bikeRequestDTO)
{
    var imageDirectory = Path.Combine("wwwroot", "images_bike");
    if (!Directory.Exists(imageDirectory))
    {
        Directory.CreateDirectory(imageDirectory);
    }

    if ( !bikeRequestDTO.BikeUnits.Any())
    {
        throw new Exception("Unit is empty.");
    }

    foreach (var bikeUnit in bikeRequestDTO.BikeUnits)
    {
        var chkReg = await _bikerepository.CheckRegNo(bikeUnit.RegistrationNumber);
        if (!chkReg)
        {
            throw new Exception("Registration Number Already Exists!");
        }
    }

    var modelId = await _bikerepository.FindModelId(bikeRequestDTO.ModelName);
    var getbikeid = await _bikerepository.AddModelBike(modelId);

    if (getbikeid <= 0)
    {
        return false;
    }

    foreach (var bikeUnt in bikeRequestDTO.BikeUnits)
    {
        var unit = new BikeUnit
        {
            BikeId = getbikeid,
            RegistrationNumber = bikeUnt.RegistrationNumber,
            Year = bikeUnt.Year,
            RentPerDay = bikeUnt.RentPerDay
        };

        var unitId = await _bikerepository.AddBikeUnit(unit);

        if (unitId <= 0)
        {
            return false;
        }

    }

    return true;
}

public async  Task <bool>AddImages([FromForm]BikeImageRequestDTO imageRequestDTO)
{
            if (imageRequestDTO.Image == null || imageRequestDTO.Image.Length == 0)
        {
            return false;
        }
         
        var bikeImages = new List<BikeImages>();

     
        var imageDirectory = Path.Combine("wwwroot", "bike_images");
        if (!Directory.Exists(imageDirectory))
        {
            Directory.CreateDirectory(imageDirectory);
        }

        var uniqueFileName = $"{Guid.NewGuid()}_{imageRequestDTO.Image.FileName}";
        var filePath = Path.Combine(imageDirectory, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageRequestDTO.Image.CopyToAsync(stream);
        }

     
        var image=new BikeImages
        {
            UnitId=imageRequestDTO.UnitId,
            Image=filePath

        };
        await _bikerepository.AddBikeImages(image);
        return true;

}


public async Task<List<Bike>>AllBikes(int pagenumber,int pagesize)
{
    var data=await _bikerepository.AllBikes(pagenumber,pagesize);

    if(data == null)
    {
        throw new Exception("Data Not Found!");
        
    }
    return data;
}

public async Task <bool>DeleteBike(string RegistrationNumber)
{
    var checkRental=await _bikerepository.checkRental(RegistrationNumber);

    if(!checkRental)
    {
        throw new Exception("Bike is Booked !");
    }

    var data=await _bikerepository.DeleteBike(RegistrationNumber);
    if(data)
    {
        return true;
    }else{
        return false;
    }
}

public async  Task <Bike>GetById(int id)
{
    var data=await _bikerepository.GetById(id);
    if(data == null)
    {
        throw new Exception("No Data!");
    }
    return data;
    
}

public async Task <Bike>GetByRegNo(string RegNo)
{
    var data=await _bikerepository.GetByRegNo(RegNo);

    if(data == null)
    {
        throw new Exception("No Such Bike!");
    }
    return data;
}

public async  Task <bool>UpdateBike(string RegistrationNumber,BikeUnit unit)
{
    var chkBike=await _bikerepository.CheckRegNo(RegistrationNumber);

    if(chkBike == true)
    {
        throw new Exception("No Such Bike!");

    }

//          var bikeImages = new List<BikeImages>();

//     var bikeunit=new BikeUnit{
//         Year=unit.Year,
//         RentPerDay=unit.RentPerDay,
//         bikeImages=bikeImages

        
   
// };
       
//         foreach (var bikeImage in unit.bikeImages)
//         {
//             var image = new BikeImages
//             {
                
//                 Image = bikeImage.Image
//             };
//             bikeImages.Add(image);


//         };


var updatebike=await _bikerepository.UpdateBike(RegistrationNumber,unit);
if(updatebike)
{
return true;
}else{
    return false;
}



 
 }

}


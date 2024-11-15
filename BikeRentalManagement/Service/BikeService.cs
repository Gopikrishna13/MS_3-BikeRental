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
            BikeId = getbikeid,
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


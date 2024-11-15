using System;
using BikeRentalManagement.Database;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.RequestDTOs;
using BikeRentalManagement.IRepository;
using BikeRentalManagement.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BikeRentalManagement.Repository;

public class BikeRepository:IBikeRepository
{
    private readonly BikeDbContext _bikeDbContext;

    public BikeRepository(BikeDbContext bikeDbContext)
    {
        _bikeDbContext=bikeDbContext;
    }

    public async Task <bool> AddBrand(Brand brand)
    {
        var findbrand=await _bikeDbContext.Brands.FirstOrDefaultAsync(b=>b.BrandName==brand.BrandName);
        if(findbrand != null)
        {
            throw new Exception("Brand Already Exists");
        }

        var data=await _bikeDbContext.Brands.AddAsync(brand);
        await _bikeDbContext.SaveChangesAsync();

        if(data != null)
        {
            return true;
        }else{
            return false;
        }
    }

    public async Task <List<Brand>>AllBrands()
    {
        var data=await _bikeDbContext.Brands.ToListAsync();
        return data;
    }

    public async Task <bool> AddModel(Model modelRequest)
    {
        var checkModel=await _bikeDbContext.Models.FirstOrDefaultAsync(m=>m.ModelName==modelRequest.ModelName);

        if(checkModel!=null)
        {
            throw new Exception ("Model Already Exists !");
        }
        var data=await _bikeDbContext.Models.AddAsync(modelRequest);
        await _bikeDbContext.SaveChangesAsync();

        if(data != null)
        {
            return true;
        }else{
            return false;
        }
    }

    public async Task <List<Brand>> GetAllModels()
    {
        var  result=await _bikeDbContext.Brands.Include(b=>b.Models).ToListAsync();
        return result;
    }

    // public async Task <bool>AddBike(BikeRequestDTO bikeRequestDTO)
    // {
       

    // }

    public async Task <bool>CheckRegNo(string RegistrationNumber)
    {
         var data=await _bikeDbContext.BikeUnits.FirstOrDefaultAsync(u=>u.RegistrationNumber==RegistrationNumber);

         if(data != null)
         {
             return false;
         }else{
            return true;
         }
    }
 public async  Task<int>FindModelId(string ModelName)
 {
    var findbike=await _bikeDbContext.Models.FirstOrDefaultAsync(m=>m.ModelName==ModelName);
    if(findbike == null)
    {
        throw new Exception("No Such Model!");
    }

    var bikeId=findbike.ModelId;
    return bikeId;
 }

public async Task<int>AddModelBike(int modelId)
{
    //can't add single value into a model
     var newBike = new Bike
    {
        ModelId = modelId
    };

    await _bikeDbContext.Bikes.AddAsync(newBike);
    await _bikeDbContext.SaveChangesAsync();
    return newBike.BikeId;

    
}

public async Task<int>AddBikeUnit(BikeUnit unit)

{
    await _bikeDbContext.BikeUnits.AddAsync(unit);
    await _bikeDbContext.SaveChangesAsync();
    return unit.UnitId;

}

public async Task <bool> AddBikeImages(List<BikeImages> bikeImages)
{
    await _bikeDbContext.BikeImages.AddRangeAsync(bikeImages);
    await _bikeDbContext.SaveChangesAsync();
    return true;
}


public async Task<List<Bike>>AllBikes(int pagenumber,int pagesize)
{
    var data=from bikeUnit in _bikeDbContext.BikeUnits
             join bikeImage in _bikeDbContext.BikeImages
             on bikeUnit.UnitId equals bikeImage.UnitId
             join bikes in _bikeDbContext.Bikes
             on bikeUnit.BikeID equals bikes.BikeId

             select new Bike
             {
                   BikeId = bikes.BikeId,
                   ModelId = bikes.ModelId,
                 
                   BikeUnits = new List<BikeUnit>{new BikeUnit{
                           RegistrationNumber = bikeUnit.RegistrationNumber,
                           Year = bikeUnit.Year,
                           RentPerDay = bikeUnit.RentPerDay,

                    bikeImages=new List<BikeImages>{
                        new BikeImages{
                            Image = bikeImage.Image
                        }
                    }

                   

                   }}
                   
                 
               

             };
               int skip=(pagenumber-1)* pagesize;
      
             var bikelist= await data.Skip(skip).Take(pagesize).ToListAsync();
             return bikelist;



}

}

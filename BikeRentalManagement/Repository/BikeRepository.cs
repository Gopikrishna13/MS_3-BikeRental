using System;
using BikeRentalManagement.Database;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.RequestDTOs;
using BikeRentalManagement.IRepository;
using Microsoft.EntityFrameworkCore;

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

    public async Task <bool>AddBike(BikeRequestDTO bikeRequestDTO)
    {
       var  result=await _bikeDbContext.Brands.Include(b=>b.Models).ToListAsync();
        return true;

    }

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

}

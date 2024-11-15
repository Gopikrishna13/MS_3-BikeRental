using System;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.RequestDTOs;

namespace BikeRentalManagement.IService;

public interface IBikeService
{
    Task <bool> AddBrand(BrandRequestDTO brandRequestDTO);
    Task <List<Brand>>AllBrands();

    Task <bool> AddModel(ModelRequestDTO modelRequestDTO);

    Task <List<Brand>> GetAllModels();

    Task <bool>AddBike(BikeRequestDTO bikeRequestDTO);

    Task<List<Bike>>AllBikes(int pagenumber,int pagesize);

    Task <bool>DeleteBike(string RegistrationNumber);

    Task <Bike>GetById(int id);

    Task <Bike>GetByRegNo(string RegNo);

    Task <bool>UpdateBike(string RegistrationNumber,BikeUnit unit);

}

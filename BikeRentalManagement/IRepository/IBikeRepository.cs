using System;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.RequestDTOs;

namespace BikeRentalManagement.IRepository;

public interface IBikeRepository
{
 Task<bool>AddBrand(Brand brandRequest);

 Task <List<Brand>>AllBrands();

 Task <bool> AddModel(Model modelRequest);

 Task <List<Brand>> GetAllModels();

 //Task <bool>AddBike(BikeRequestDTO bikeRequestDTO);
 Task <bool>CheckRegNo(string RegistrationNumbers);
 Task<int>FindModelId(string ModelName);

 //Task<int>getbikeId();

Task <bool>AddBikeImages(List<BikeImages> bikeImages);
Task <int>AddBikeUnit(BikeUnit unit);
Task <int> AddModelBike(int modelId);
Task<List<Bike>>AllBikes(int pagenumber,int pagesize);
Task<bool>checkRental(string RegistrationNumber);
Task<bool>DeleteBike(string RegistrationNumber);
Task <Bike>GetById(int id);
Task <Bike>GetByRegNo(string RegNo);
}

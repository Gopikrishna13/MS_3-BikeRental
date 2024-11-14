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

 Task <bool>AddBike(BikeRequestDTO bikeRequestDTO);
 Task <bool>CheckRegNo(string RegistrationNumbers);
}

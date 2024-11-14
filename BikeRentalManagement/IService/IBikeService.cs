using System;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.RequestDTOs;

namespace BikeRentalManagement.IService;

public interface IBikeService
{
    Task <bool> AddBrand(BrandRequestDTO brandRequestDTO);
    Task <List<Brand>>AllBrands();

    Task <bool> AddModel(ModelRequestDTO modelRequestDTO);

}

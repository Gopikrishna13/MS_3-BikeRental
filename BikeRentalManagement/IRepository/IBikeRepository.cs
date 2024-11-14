using System;
using BikeRentalManagement.Database.Entities;

namespace BikeRentalManagement.IRepository;

public interface IBikeRepository
{
 Task<bool>AddBrand(Brand brandRequest);

 Task <List<Brand>>AllBrands();

 Task <bool> AddModel(Model modelRequest);
}

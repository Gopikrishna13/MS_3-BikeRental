using System;
using BikeRentalManagement.Database;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalManagement.Repository;

public class ReportRepository:IReportRepository
{
    private readonly BikeDbContext _bikeDbContext;

    public ReportRepository(BikeDbContext bikeDbContext)
    {
        _bikeDbContext=bikeDbContext;
    }
    public async  Task<int>TotalUsers()
     {
        var data=await _bikeDbContext.Users.CountAsync();
        return data;

     }

     public async Task <int>TotalBikes()
     {
        var data=await _bikeDbContext.BikeUnits.CountAsync();
        return data;
     }

     public async Task<int> TotalBooked()
     {
        var data=await _bikeDbContext.RentalRequests.Where(r=>r.Status == Status.Pending).CountAsync();
        return data;
     }

     public async  Task <int>Revenue()
     {
        var data=await _bikeDbContext.RentalRequests.Where(r=>r.Status == Status.Returned).Select(r=>r.Amount).SumAsync();
        return data;
     }

     public async Task<object>GetRevenueByMonth()
     {
      var data=await _bikeDbContext.RentalRequests.ToListAsync();
      
        var list =  from i in data
    group i by i.ToDate.ToString("MMM") into grp
    select new {Month = grp.Key, Count = grp.Sum(i => i.Amount)};

    return list;

     }
  public async Task<object> GetRevenueByBike()
{
    var data = await (
        from rental in _bikeDbContext.RentalRequests
        join bike in _bikeDbContext.Bikes on rental.BikeId equals bike.BikeId
        join model in _bikeDbContext.Models on bike.ModelId equals model.ModelId
        group rental by model.ModelName into grp
        select new
        {
            ModelName = grp.Key,
            Revenue = grp.Sum(r => r.Amount)
        }
    ).ToListAsync();

    return data;
}

   
}

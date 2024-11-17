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

}

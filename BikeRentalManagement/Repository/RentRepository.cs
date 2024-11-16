using System;
using System.Net.WebSockets;
using BikeRentalManagement.Database;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.ResponseDTOs;
using BikeRentalManagement.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalManagement.Repository;

public class RentRepository:IRentRepository
{
    private readonly BikeDbContext _bikeDbContext;

    public RentRepository(BikeDbContext bikeDbContext)
    {
        _bikeDbContext=bikeDbContext;
    }
    public async Task<bool> RequestRent(RentalRequest rentRequest)
    {
        var data=await _bikeDbContext.RentalRequests.AddAsync(rentRequest);
        await _bikeDbContext.SaveChangesAsync();
        if(data != null)
        {
          return true;
        }else{
            return false;
        }
    

    }

    // public async Task <bool>CheckRequest(DateTime FromDate,DateTime ToDate)
    // {

    // }

    public async Task<List<BookedDatesResponseDTO>> GetBikeBookedDates(string registrationNumber)
    {
        var data=await _bikeDbContext.RentalRequests
        .Where(r=>r.RegistrationNumber==registrationNumber && r.Status.Equals("Pending")).ToListAsync();

        var date=new List<BookedDatesResponseDTO>();
        foreach(var d in data)
        {
           var bookedDatesDto = new BookedDatesResponseDTO
        {
            Dates = new List<DateTime> { d.FromDate, d.ToDate }
        };

        date.Add(bookedDatesDto);
        }
        
        return date;
    }

}

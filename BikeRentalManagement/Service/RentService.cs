using System;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.RequestDTOs;
using BikeRentalManagement.DTOs.ResponseDTOs;
using BikeRentalManagement.IRepository;
using BikeRentalManagement.IService;

namespace BikeRentalManagement.Service;

public class RentService:IRentService
{
    private readonly IRentRepository _rentRepository;

    public RentService(IRentRepository rentRepository)
    {
        _rentRepository=rentRepository;
    }
    public async Task<bool>RequestRent(RentRequestDTO rentRequestDTO)
     {
        var rentRequest=new RentalRequest
        {
            UserId=rentRequestDTO.UserId,
            BikeId=rentRequestDTO.BikeId,
            RegistrationNumber=rentRequestDTO.RegistrationNumber,
            FromDate=rentRequestDTO.FromDate,
            ToDate=rentRequestDTO.ToDate,
            FromLocation=rentRequestDTO.FromLocation,
            ToLocation=rentRequestDTO.ToLocation,
            Distance=rentRequestDTO.Distance,
            Amount=rentRequestDTO.Amount,
            Status=rentRequestDTO.Status

        };

        var checkRequest=await _rentRepository.CheckRequest(rentRequestDTO.RegistrationNumber,rentRequestDTO.FromDate,rentRequestDTO.ToDate);

        if(checkRequest)
        {
            throw new Exception("Already Booked!");
        }

        var data=await _rentRepository.RequestRent(rentRequest);

        if(data)
        {
            return true;
        }else{
            return false;
        }

     }

     public async Task<List<BookedDatesResponseDTO>>GetBikeBookedDates(string RegistrationNumber)
     {

        var data=await _rentRepository.GetBikeBookedDates(RegistrationNumber);

        if(data == null)
        {
            return new List<BookedDatesResponseDTO>();
        }

        return data;

     }
 public async Task <List<RentalRequest>> AllRequest()
 {
    var data=await _rentRepository.AllRequest();
    if(data == null)
    {
        throw new Exception("Nothing Found!");
    }
    return data;

 }

}

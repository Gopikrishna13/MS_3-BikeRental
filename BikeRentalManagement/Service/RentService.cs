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
 public async Task <List<AllRentalResponseDTO>> AllRequest()
 {
    var data=await _rentRepository.AllRequest();
    if(data == null)
    {
        throw new Exception("Nothing Found!");
    }

    var response=new List<AllRentalResponseDTO>();

    foreach(var d in data)
    {
        var rentalresponse=new AllRentalResponseDTO
        {
            RequestId=d.RequestId,
            UserId=d.UserId,
            BikeId=d.BikeId,
            RegistrationNumber=d.RegistrationNumber,
            FromDate=d.FromDate,
            ToDate=d.ToDate,
            FromLocation=d.FromLocation,
            ToLocation=d.ToLocation,
            Distance=d.Distance,
            Amount=d.Amount,
            Due=d.Due,
            Status=d.Status.ToString()


        };

        response.Add(rentalresponse);
    }
    return response;

 }

 public async  Task <RentalRequest>GetRequestById(int id)
 {
    var data=await _rentRepository.GetRequestById(id);

    if(data == null)
    {
        throw new Exception("No Data Found!");
    }
    return data;
 }
 public async  Task<bool>AcceptRejectRequest(int id,int status)
 {
    var data=await _rentRepository.AcceptRejectRequest(id,status);
    if(data)
    {
        return true;
    }else{
        return false;
    }
 }

 public async Task<bool>CancelRequest(int id)
 {
    var data=await _rentRepository.CancelRequest(id);
    if(data)
    {
        return true;
    }else{
        return false;
    }
 }

 public async  Task <bool> UpdateRequest(int id,RentRequestDTO rentRequestDTO)
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

    var data=await _rentRepository.UpdateRequest(id,rentRequest);

    if(data)
    {
        return true;
    }else{
        return false;
    }
 }


 public async Task <List<RentalResponseDTO>>RequestByUser(int id)
 {
    var data=await _rentRepository.RequestByUser(id);
    if(data == null)
    {
        throw new Exception("No Data Found !");
    }
    return data;
 }

public async Task <ICollection<object>>CountHistory(int id)
{
    var data=await _rentRepository.CountHistory(id);

    if(data == null)
    {
        
    }
    return data;
}

public async Task <bool>LateReturns()
{
    var data=await _rentRepository.LateReturns();
    return data;
}

}

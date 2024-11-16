using System;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.ResponseDTOs;

namespace BikeRentalManagement.IRepository;

public interface IRentRepository
{
    Task<List<BookedDatesResponseDTO>> GetBikeBookedDates(string registrationNumber);
    Task<bool> RequestRent(RentalRequest rentRequest);
   Task <bool>CheckRequest(string RegistrationNumber,DateTime FromDate,DateTime ToDate);
}

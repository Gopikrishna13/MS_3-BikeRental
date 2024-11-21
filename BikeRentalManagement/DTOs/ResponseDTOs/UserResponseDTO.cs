using System;
using System.ComponentModel.DataAnnotations;
using BikeRentalManagement.Database.Entities;

namespace BikeRentalManagement.DTOs.ResponseDTOs;

public class UserResponseDTO
{
    public int UserId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string MobileNumber { get; set; }

 
    public string NIC { get; set; }

    // [Required]
    // public string Password { get; set; }

 
    public string LicenseNumber { get; set; }

    public string Password{get;set;}

 
    public string LicenseImage { get; set; }


    public string CameraCapture { get; set; }
}

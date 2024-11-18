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

    [Required]
    public string NIC { get; set; }

    // [Required]
    // public string Password { get; set; }

    [Required]
    public string LicenseNumber { get; set; }

    [Required]
    public Role Role { get; set; }

    [Required]
    public string LicenseImage { get; set; }

    [Required]
    public string CameraCapture { get; set; }
}

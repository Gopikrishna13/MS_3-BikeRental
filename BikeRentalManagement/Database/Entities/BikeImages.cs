using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeRentalManagement.Database.Entities;

public class BikeImages
{

    [Key]
    public int ImageId{get;set;}

    [ForeignKey("BikeUnit")]
    public int UnitId{get;set;}


    [Required]
    public byte[] Image{get;set;}

    public BikeUnit?bikeUnit{get;set;}

}

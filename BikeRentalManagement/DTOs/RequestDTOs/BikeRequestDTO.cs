using System;
using BikeRentalManagement.Database.Entities;

namespace BikeRentalManagement.DTOs.RequestDTOs;

public class BikeRequestDTO
{
 public string ModelName { get; set; }
public List<BikeUnit> BikeUnits { get; set; } = new List<BikeUnit>();
    
}

using System;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.RequestDTOs;
using BikeRentalManagement.DTOs.ResponseDTOs;

namespace BikeRentalManagement.IService;

public interface IUserService
{
Task <bool> CreateUser(UserRequestDTO userRequestDTO);
Task<List<User>>AllUsers(int pagenumber,int pagesize);
Task <UserResponseDTO>UserById(int Id);
Task <bool> DeleteById(int Id);

Task <bool> UpdateUser(int Id,UserRequestDTO userRequestDTO);


Task <string> Login(LoginRequestDTO loginrequest);
Task <bool> AddEmail(Email mail);
}

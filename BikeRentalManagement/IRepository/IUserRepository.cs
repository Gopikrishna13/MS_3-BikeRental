using System;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.RequestDTOs;
using BikeRentalManagement.Repository;

namespace BikeRentalManagement.IRepository;

public interface IUserRepository
{
Task <bool> CreateUser(User user);

Task<List<User>>AllUsers(int pagenumber,int pagesize);

Task <User>UserById(int Id);
Task <bool> DeleteById(int Id);

Task <bool> UpdateUser(int Id,User user);

Task <string> Login(LoginRequestDTO loginrequest);
Task <bool> AddEmail(Email mail);
}

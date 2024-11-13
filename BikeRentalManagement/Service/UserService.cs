using System;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.RequestDTOs;
using BikeRentalManagement.DTOs.ResponseDTOs;
using BikeRentalManagement.IRepository;
using BikeRentalManagement.IService;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BikeRentalManagement.Service;

public class UserService:IUserService
{

 private readonly  IUserRepository _userRepository;

public UserService(IUserRepository userRepository)
{
    _userRepository=userRepository;
}
    public async Task <bool> CreateUser(UserRequestDTO userRequestDTO)
    {

        var user=new User {
            FirstName=userRequestDTO.FirstName,
            LastName=userRequestDTO.LastName,
            Email=userRequestDTO.Email,
            MobileNumber=userRequestDTO.MobileNumber,
            NIC=userRequestDTO.NIC,
            Password=userRequestDTO.Password,
            LicenseNumber=userRequestDTO.LicenseNumber,
            Role=userRequestDTO.Role,
            LicenseImage=Convert.FromBase64String(userRequestDTO.LicenseImage),
            CameraCapture=Convert.FromBase64String(userRequestDTO.CameraCapture)


        };
  user.Password=BCrypt.Net.BCrypt.HashPassword(user.Password);
        var data=await _userRepository.CreateUser(user);
        if(data)
        {
            return (true);
        }else{
            return false;
        }

    }

    public async Task<List<User>>AllUsers()
    {
        var data=await _userRepository.AllUsers();

        if(data == null)
        {
            throw new Exception("No Data Exists!");
        }else{
            return data;
        }
    }
    public async Task <UserResponseDTO>UserById(int Id)
    {

        var user=await _userRepository.UserById(Id);

        var response=new UserResponseDTO
        {
            UserId=user.UserId,
            FirstName=user.FirstName,
            LastName=user.LastName,
            Email=user.Email,
            MobileNumber=user.MobileNumber,
            NIC=user.NIC,
            LicenseNumber=user.LicenseNumber,
            Role=user.Role,
            LicenseImage=user.LicenseImage,
            CameraCapture=user.CameraCapture


        };
        return response;

       
        
    }

    public async Task <bool> DeleteById(int Id)
    {
        var data=await _userRepository.DeleteById(Id);

       return data;
    }

    public async Task <bool> UpdateUser(int Id ,UserRequestDTO userRequestDTO)
    {

         var user=new User {
            FirstName=userRequestDTO.FirstName,
            LastName=userRequestDTO.LastName,
            Email=userRequestDTO.Email,
            MobileNumber=userRequestDTO.MobileNumber,
            NIC=userRequestDTO.NIC,
            Password=userRequestDTO.Password,
            LicenseNumber=userRequestDTO.LicenseNumber,
            Role=userRequestDTO.Role,
            LicenseImage=Convert.FromBase64String(userRequestDTO.LicenseImage),
            CameraCapture=Convert.FromBase64String(userRequestDTO.CameraCapture)


        };
        var data=await _userRepository.UpdateUser(Id,user);

       return data;
    }

    public async Task <string> Login(LoginRequestDTO loginrequest)
    {

        var data=await _userRepository.Login(loginrequest);

       return data;

    }

}

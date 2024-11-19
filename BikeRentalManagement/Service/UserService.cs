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
  public async Task<bool> CreateUser(UserRequestDTO userRequestDTO)
{
    try
    {
       
        var imageDirectory = Path.Combine("wwwroot", $"images");
        if (!Directory.Exists(imageDirectory))
        {
            Directory.CreateDirectory(imageDirectory);
        }

      
        var imagePath = Path.Combine(imageDirectory, userRequestDTO.LicenseImage.FileName);
        if (userRequestDTO.LicenseImage != null && userRequestDTO.LicenseImage.Length > 0)
        {
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await userRequestDTO.LicenseImage.CopyToAsync(stream);
            }
        }

       
        var imagePathCam = Path.Combine(imageDirectory, userRequestDTO.CameraCapture.FileName);
        if (userRequestDTO.CameraCapture != null && userRequestDTO.CameraCapture.Length > 0)
        {
            using (var stream = new FileStream(imagePathCam, FileMode.Create))
            {
                await userRequestDTO.CameraCapture.CopyToAsync(stream);
            }
        }

     
        var user = new User
        {
            FirstName = userRequestDTO.FirstName,
            LastName = userRequestDTO.LastName,
            Email = userRequestDTO.Email,
            MobileNumber = userRequestDTO.MobileNumber,
            NIC = userRequestDTO.NIC,
            Password = userRequestDTO.Password,
            LicenseNumber = userRequestDTO.LicenseNumber,
            Role = userRequestDTO.Role,
            LicenseImage = imagePath,
            CameraCapture = imagePathCam,
            Status = userRequestDTO.Status
        };

        
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

     
        var data = await _userRepository.CreateUser(user);
        if(data == true )
        {
            return true;
        }else{
            return false;
        }
    }
    catch (Exception ex)
    {
   
        Console.WriteLine(ex.Message);
        return false;
    }
}


    public async Task<bool>UserRequest(int id,int status)
    {
        var data=await _userRepository.UserRequest(id,status);
        if(data)
        {
            return true;
        }else{
            return false;
        }
    }

    public async Task<List<User>>AllUsers(int pagenumber,int pagesize)
    {
        var data=await _userRepository.AllUsers(pagenumber,pagesize);

        if(data == null)
        {
            throw new Exception("No Data Exists!");
        }else{
            return data;
        }
    }
public async Task<UserResponseDTO> UserById(int Id)
{
    try
    {
       
        var user = await _userRepository.UserById(Id);

        if (user == null)
        {
            return null; 
        }

        var response = new UserResponseDTO
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            MobileNumber = user.MobileNumber,
            NIC = user.NIC,
            LicenseNumber = user.LicenseNumber,
            Role = user.Role,
            LicenseImage = user.LicenseImage,
            CameraCapture = user.CameraCapture
        };

        return response;
    }
    catch (Exception ex)
    {
        
        Console.WriteLine(ex.Message);
        return null;
    }
}


    public async Task <bool> DeleteById(int Id)
    {
        var data=await _userRepository.DeleteById(Id);

       return data;
    }

   public async Task<bool> UpdateUser(int Id, UserRequestDTO userRequestDTO)
{
    try
    {
        
        var existingUser = await _userRepository.UserById(Id);
        if (existingUser == null)
        {
            return false; 
        }

        var imageDirectory = Path.Combine("wwwroot", "images");
        if (!Directory.Exists(imageDirectory))
        {
            Directory.CreateDirectory(imageDirectory);
        }

        string imagePath = existingUser.LicenseImage;
        string imagePathCam = existingUser.CameraCapture;

    
        if (userRequestDTO.LicenseImage != null && userRequestDTO.LicenseImage.Length > 0)
        {
            imagePath = Path.Combine(imageDirectory, userRequestDTO.LicenseImage.FileName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await userRequestDTO.LicenseImage.CopyToAsync(stream);
            }
        }

      
        if (userRequestDTO.CameraCapture != null && userRequestDTO.CameraCapture.Length > 0)
        {
            imagePathCam = Path.Combine(imageDirectory, userRequestDTO.CameraCapture.FileName);
            using (var stream = new FileStream(imagePathCam, FileMode.Create))
            {
                await userRequestDTO.CameraCapture.CopyToAsync(stream);
            }
        }

     
        var user = new User
        {
            UserId = Id,
            FirstName = userRequestDTO.FirstName ?? existingUser.FirstName,
            LastName = userRequestDTO.LastName ?? existingUser.LastName,
            Email = userRequestDTO.Email ?? existingUser.Email,
            MobileNumber = userRequestDTO.MobileNumber ?? existingUser.MobileNumber,
            NIC = userRequestDTO.NIC ?? existingUser.NIC,
            Password = userRequestDTO.Password,
            LicenseNumber = userRequestDTO.LicenseNumber ?? existingUser.LicenseNumber,
            Role = userRequestDTO.Role,
            LicenseImage = imagePath,
            CameraCapture = imagePathCam,
            Status =  existingUser.Status
        };

      
        var data = await _userRepository.UpdateUser(Id, user);
        return data;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        return false;
    }
}

    public async Task <string> Login(LoginRequestDTO loginrequest)
    {

        var data=await _userRepository.Login(loginrequest);

       return data;

    }

    public async Task <bool> AddEmail(Email mail)
    {
        var data=await _userRepository.AddEmail(mail);
        if(data)
        {
            return true;
        }else{
            return false;
        }
    }

}

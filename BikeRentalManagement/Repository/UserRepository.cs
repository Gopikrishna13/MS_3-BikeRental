using System;
using BikeRentalManagement.Database;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.RequestDTOs;
using BikeRentalManagement.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalManagement.Repository;

public class UserRepository:IUserRepository
{
    private readonly BikeDbContext _bikeDbContext;

    public UserRepository(BikeDbContext bikeDbContext)
    {
        _bikeDbContext=bikeDbContext;

    }

    public async Task <bool> CreateUser(User user)
    {
        var checkUser=await _bikeDbContext.Users.FirstOrDefaultAsync(u=>u.Email==user.Email || u.NIC==user.NIC || u.LicenseNumber==user.LicenseNumber);
        if(checkUser!=null)
        {
            throw new Exception("User already Exists!");
        }

        var data=await _bikeDbContext.Users.AddAsync(user);
        await _bikeDbContext.SaveChangesAsync();

        if(data!=null)
        {
            return true;

        }else{
            return false;
        }

    }

    public async Task<List<User>>AllUsers()
    {
        var data=await _bikeDbContext.Users.ToListAsync();
        return data;
    }

    public async Task <User>UserById(int Id)
    {
        var data=await _bikeDbContext.Users.FirstOrDefaultAsync(u=>u.UserId==Id);

        if(data == null)
        {
            throw new Exception("No User Found!");
        }
        return data;
    }

    public async Task <bool> DeleteById(int Id)
    {
         var data=await _bikeDbContext.Users.FindAsync(Id);

         if(data == null)
         {
            throw new Exception("No Such User!");
         }

         var deleteuser= _bikeDbContext.Users.Remove(data);
         await _bikeDbContext.SaveChangesAsync();

         if(deleteuser != null)
         {
            return true;
         }else{
            return false;
         }

    }

public async Task<bool> UpdateUser(int Id, User user)
{
    var existingUser = await _bikeDbContext.Users.FirstOrDefaultAsync(u => u.UserId == Id );

    if (existingUser == null)
    {
        throw new Exception("No such User!");
    }

 

 var checkUser=await _bikeDbContext.Users.FirstOrDefaultAsync(u=>u.Email==user.Email && u.UserId!=Id);
        if(checkUser!=null)
        {
            throw new Exception("Email already Exists!");
        }
    existingUser.FirstName = user.FirstName;
    existingUser.LastName = user.LastName;
    existingUser.Email = user.Email;
    existingUser.MobileNumber = user.MobileNumber;
    existingUser.NIC = user.NIC;
    existingUser.Password = user.Password;
    existingUser.LicenseNumber = user.LicenseNumber;
    existingUser.Role = user.Role;
    existingUser.LicenseImage = user.LicenseImage;
    existingUser.CameraCapture = user.CameraCapture;

_bikeDbContext.Entry(existingUser).State=EntityState.Modified;



   

    try
    {
        await _bikeDbContext.SaveChangesAsync();
        _bikeDbContext.Entry(existingUser).Reload();

       
    }
    catch (DbUpdateConcurrencyException)
    {

        throw ;
    }

    return true;
}

public async Task <bool> Login(LoginRequestDTO loginrequest)
{
    var data=await _bikeDbContext.Users.FirstOrDefaultAsync(u=>u.Email==loginrequest.Email);

    if(data == null)
    {
        throw new Exception("Invalid Email ID!");
    }

    if(!BCrypt.Net.BCrypt.Verify(loginrequest.Password,data.Password))
    {
        throw new Exception("Wrong Password");
    }

    return true;
}



}

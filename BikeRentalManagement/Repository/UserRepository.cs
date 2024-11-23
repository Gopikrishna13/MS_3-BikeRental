using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BikeRentalManagement.Database;
using BikeRentalManagement.Database.Entities;
using BikeRentalManagement.DTOs.RequestDTOs;
using BikeRentalManagement.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace BikeRentalManagement.Repository;

public class UserRepository:IUserRepository
{
    private readonly BikeDbContext _bikeDbContext;
     private readonly IConfiguration _configuration;

    public UserRepository(BikeDbContext bikeDbContext,IConfiguration configuration)
    {
        _bikeDbContext=bikeDbContext;
        _configuration = configuration;

    }

public async Task<bool> CreateUser(User user)
{
 
    var checkUser = await _bikeDbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email || u.NIC == user.NIC || u.LicenseNumber == user.LicenseNumber);
    if (checkUser != null)
    {
        throw new Exception("User already exists!");
    }

    var data = await _bikeDbContext.Users.AddAsync(user);
    var rows=await _bikeDbContext.SaveChangesAsync();

 
        return rows > 0;

   
   
}

public async Task<bool>UserRequest(int id,int status)
{
     var data=await _bikeDbContext.Users.FirstOrDefaultAsync(u=>u.UserId == id);
     if(data == null)
     {
        throw new Exception("No Such User!");
     }
  
   if(status==5)
   {
    data.Status=Status.Rejected;
    await _bikeDbContext.SaveChangesAsync();
     var emailMessageRej = new MimeMessage();
    emailMessageRej.From.Add(new MailboxAddress("No-Reply", "Me2@gmail.com"));
    emailMessageRej.To.Add(new MailboxAddress("", data.Email));
    emailMessageRej.Subject = "Request Reject!";
    emailMessageRej.Body = new TextPart("plain")
    {
        Text = $" {data.FirstName}\n Your Request has been rejected!"
    };

    using (var client = new SmtpClient())
    {
        try
        {
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("sivapakthangopikrishna69@gmail.com", "plev rbuw jsgh iipc");
            await client.SendAsync(emailMessageRej);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;  
        }
    }
     var emailRej = await _bikeDbContext.Emails.FirstOrDefaultAsync(e => e.EmailType == EmailType.UserPassword);
    if (emailRej == null)
    {
        throw new Exception("Failed to get Email!");
    }

   
    var notificationRej = new Notification
    {
        UserId = data.UserId,   
        EmailId = emailRej.EmailId,
        Date = DateTime.UtcNow 
    };

  
    _bikeDbContext.Notifications.Add(notificationRej);

  Console.WriteLine(data.UserId+""+emailRej.EmailId);
    var resultRej = await _bikeDbContext.SaveChangesAsync();
    _bikeDbContext.Entry(notificationRej).Reload();
    
    return false;
   }
     data.Status=Status.Accepted;
     await _bikeDbContext.SaveChangesAsync();

    var emailMessage = new MimeMessage();
    emailMessage.From.Add(new MailboxAddress("No-Reply", "Me2@gmail.com"));
    emailMessage.To.Add(new MailboxAddress("", data.Email));
    emailMessage.Subject = "Activate Account!";
    emailMessage.Body = new TextPart("plain")
    {
        Text = $"Welcome {data.FirstName}\n Thank you for registering on our site.\nYour Username: {data.Email}\nYour Password: {12345678}\nPlease update your password!"
    };

    using (var client = new SmtpClient())
    {
        try
        {
            await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("sivapakthangopikrishna69@gmail.com", "plev rbuw jsgh iipc");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;  
        }
    }

    var email = await _bikeDbContext.Emails.FirstOrDefaultAsync(e => e.EmailType == EmailType.UserPassword);
    if (email == null)
    {
        throw new Exception("Failed to get Email!");
    }

   
    var notification = new Notification
    {
        UserId = data.UserId,   
        EmailId = email.EmailId,
        Date = DateTime.UtcNow 
    };

  
    _bikeDbContext.Notifications.Add(notification);

  Console.WriteLine(data.UserId+""+email.EmailId);
    var result = await _bikeDbContext.SaveChangesAsync();
    _bikeDbContext.Entry(notification).Reload();

    return result > 0;

}


    public async Task<List<User>>AllUsers(int pagenumber,int pagesize)
    {
        int skip=(pagenumber-1)* pagesize;
        var data=await _bikeDbContext.Users.Skip(skip).Take(pagesize).ToListAsync();
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
         var data=await _bikeDbContext.Users.FirstOrDefaultAsync(u=>u.UserId==Id);

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
    existingUser.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
    existingUser.LicenseNumber = user.LicenseNumber;
    //
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

public async Task <string> Login(LoginRequestDTO loginrequest)
{
    var data=await _bikeDbContext.Users.FirstOrDefaultAsync(u=>u.Email==loginrequest.Email && u.Status==Status.Accepted);

    if(data == null)
    {
        throw new Exception("Invalid Email ID!");
    }

    if(!BCrypt.Net.BCrypt.Verify(loginrequest.Password,data.Password))
    {
        throw new Exception("Wrong Password");
    }

var token=CreateToken(data);
Console.WriteLine(token);
    return token;
}


 private string CreateToken(User user)
{
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

   
    var claims = new[]
    {
        
        new Claim("userId", user.UserId.ToString()),
        new Claim("email", user.Email),
        new Claim("role", user.Role.ToString())
    };


    var token = new JwtSecurityToken(
        issuer: "Me2",
        audience: "Users",
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(30),
        signingCredentials: credentials
    );

  
    return new JwtSecurityTokenHandler().WriteToken(token);
}

public async Task <bool> AddEmail(Email mail)
{
    var data=await _bikeDbContext.Emails.AddAsync(mail);
    await _bikeDbContext.SaveChangesAsync();
    if(data != null)
    {
        return true;
    }else{
        return false;
    }
    
}

}

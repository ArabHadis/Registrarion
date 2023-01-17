using System.Net;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Registration.Entities;
using Registration.Models;
using Registration.Utilities;

namespace Registration.RegistrationServices;

public interface IRegistrationService
{
    Task<Guid> InsertUser(UserInsertModel userInsertModel);
    Task<List<UsersGetModel>> GetUsers();
    Task<bool> SendEmail(string receiverEmail);
}
public class RegistrationService : IRegistrationService
{
    private readonly RegistrationDbContext _context;

    public RegistrationService(RegistrationDbContext context)
    {
        _context = context;
    }


    public async Task<Guid> InsertUser(UserInsertModel userInsertModel)
    {
        var user = CreateUser(userInsertModel);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user.Id;
    }

    public async Task<List<UsersGetModel>> GetUsers()
    {
        var users = await _context.Users.Select(u => new UsersGetModel
        {
            FullName = u.FirstName + " " + u.LastName,
            Email = u.Email
        })
            .ToListAsync();

        return users;
    }

    public async Task<bool> SendEmail(string receiverEmail)
    {
        //try
        //{
        //    var senderEmail = "hadisearab890@gmail.com";
        //    MailMessage mail = new MailMessage();
        //    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

        //    mail.From = new MailAddress(senderEmail);
        //    mail.To.Add(receiverEmail);

        //    mail.Body = "ثبت نام شما با موفقیت انجام شد";
        //    SmtpServer.Port = 587;
        //    SmtpServer.Credentials = new NetworkCredential("Hadise Arab", "Hadis1377");
        //    SmtpServer.EnableSsl = true;

        //    SmtpServer.Send(mail);

        //    return true;
        //}
        //catch (Exception e)
        //{
        //    throw new Exception(e.Message);
        //}

        //var mailMessage = new MimeMessage();
        //mailMessage.From.Add(new MailboxAddress("from name", "from email"));
        //mailMessage.To.Add(new MailboxAddress("to name", "to email"));
        //mailMessage.Subject = "subject";
        //mailMessage.Body = new TextPart("plain")
        //{
        //    Text = "Hello"
        //};

        //using (var smtpClient = new SmtpClient())
        //{
        //    smtpClient.Connect("smtp.gmail.com", 587, true);
        //    smtpClient.Port = 587;
        //    smtpClient.("user", "password");
        //    smtpClient.Send(mailMessage);
        //    smtpClient.Disconnect(true);
        //}

        string to = receiverEmail;
        string from = "hadisearab890@gmail.com";
        MailMessage message = new MailMessage(from, to);
        message.Subject = "test";
        message.Body = @"برای تست";
        SmtpClient client = new SmtpClient("mtp.gmail.com");
        // Credentials are necessary if the server requires the client
        // to authenticate before it will send email on the client's behalf.
        client.UseDefaultCredentials = true;

        try
        {
            client.Send(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                ex.ToString());
        }

        return true;

    }

    private static User CreateUser(UserInsertModel userInsertModel)
    {
        var user = new User
        {
            NationalCode = userInsertModel.NationalCode.Trim(),
            UserName = userInsertModel.UserName,
            FirstName = userInsertModel.FirstName.Trim(),
            LastName = userInsertModel.LastName.Trim(),
            MobileNo = userInsertModel.MobileNo?.Trim(),
            Picture = userInsertModel.Picture,
            Email = userInsertModel.Email.Trim(),
            Gender = userInsertModel.Gender,
            Password = userInsertModel.Password.Trim().HashPass()
        };

        return user;
    }
}
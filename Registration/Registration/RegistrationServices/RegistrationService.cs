using Registration.Entities;
using Registration.Models;
using Registration.Utilities;

namespace Registration.RegistrationServices;

public interface IRegistrationService
{
    Task<Guid> InsertUser(UserInsertModel userInsertModel);
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

    private static User CreateUser(UserInsertModel userInsertModel)
    {
        var user = new User
        {
            NationalCode = userInsertModel.NationalCode.Trim(),
            FirstName = userInsertModel.FirstName.Trim(),
            LastName = userInsertModel.LastName.Trim(),
            MobileNo = userInsertModel.MobileNo.Trim(),
            Picture = userInsertModel.Picture,
            Email = userInsertModel.Email.Trim(),
            Gender = userInsertModel.Gender,
            Password = userInsertModel.Password.Trim().HashPass()
        };

        return user;
    }
}
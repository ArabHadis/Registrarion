using Registration.Entities;
using Registration.Enums;

namespace Registration.Models;

public class UserInsertModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MobileNo { get; set; }
    public string Picture { get; set; }
    public string Email { get; set; }
    public string NationalCode { get; set; }
    public Gender Gender { get; set; }
    public string Password { get; set; }

}
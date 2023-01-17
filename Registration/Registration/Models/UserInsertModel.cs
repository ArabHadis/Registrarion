using System.ComponentModel.DataAnnotations;
using Registration.Entities;
using Registration.Enums;

namespace Registration.Models;

public class UserInsertModel
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string UserName { get; set; }

    [StringLength(11)]
    public string? MobileNo { get; set; }
    public string? Picture { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string NationalCode { get; set; }
    public Gender Gender { get; set; }
    public string Password { get; set; }

}
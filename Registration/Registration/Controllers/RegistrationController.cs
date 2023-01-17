using Microsoft.AspNetCore.Mvc;
using Registration.Models;
using Registration.RegistrationServices;

namespace Registration.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class RegistrationController : ControllerBase
    {
       private readonly IRegistrationService _registrationService;

       public RegistrationController(IRegistrationService registrationService)
       {
           _registrationService = registrationService;
       }


       [HttpPost]
       public async Task<IActionResult> InsertUser(UserInsertModel userInsertModel)
       {
           var result = await _registrationService.InsertUser(userInsertModel);

           return Ok(result);
       }

       [HttpGet]
       public async Task<IActionResult> GetUsers()
       {
           var result = await _registrationService.GetUsers();

           return Ok(result);
       }

       [HttpPost]
       public async Task<IActionResult> SendEmail(string receiverEmail)
        {
           var result = await _registrationService.SendEmail(receiverEmail);

           return Ok(result);
       }
    }
}
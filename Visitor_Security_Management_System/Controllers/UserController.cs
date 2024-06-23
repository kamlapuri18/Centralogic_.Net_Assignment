using Microsoft.AspNetCore.Mvc;
using Visitor_Security_Clearance_System.DTO;
using Visitor_Security_Clearance_System.Interface;


namespace Visitor_Security_Clearance_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }



        [HttpPost("login")]
         public async Task<ActionResult<UserLoginDTO>> Login([FromBody] UserLoginDTO loginRequest)
         {
             try
             {
                 var userLoginDTO = await _authService.Authenticate(loginRequest.Email);

                 if (userLoginDTO != null)
                 {
                     if (userLoginDTO.Role == loginRequest.Role)
                     {
                         return Ok(new { message = "Login successful" });
                     }
                     else
                     {
                         return Unauthorized(new { message = "Invalid role" });
                     }
                 }
                 else
                 {
                     return Unauthorized(new { message = "Invalid credentials" });
                 }
             }
             catch (Exception ex)
             {
                 return StatusCode(500, new { message = "An error occurred " });
             }
         }
    }
}

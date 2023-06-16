using Application.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private const string TokenSecret = "MySuperSecretToken";
        private static readonly TimeSpan TokenLifetime = TimeSpan.FromHours(1);
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        internal static string GetSHA512Hash(string input)
        {
            using (var sha512 = System.Security.Cryptography.SHA512.Create())
            {
                var hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        //[HttpPost("")]
        //public IActionResult GenerateToken
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                //First we need to check if the user exists in the database
                //For this we need to convert password to hash
                //Then we need to check if the user exists in the database
                //If the user exists, we need to generate a token
                //We need to return the token to the client

                string hashedPassword = GetSHA512Hash(password);

                var command = new Login();
                command.Username = username;
                command.Password = hashedPassword;
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPost("logout")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(500)]
        //public IActionResult PostLogout()
        //{
        //    try
        //    {
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}

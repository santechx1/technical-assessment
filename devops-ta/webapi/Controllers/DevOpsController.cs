using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevOpsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public DevOpsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public DevOpsResponse Post([FromBody] DevOpsRequest request)
        {
            Response.Headers.Add("X-JWT-KWY", GenerateToken(request));
            return new DevOpsResponse() { Message = $"Hello {request.To} your message will be send" };
        }

        private string GenerateToken(DevOpsRequest request)
        {
            string secret = _config["JwtSecret"];
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, request.From),
                    new Claim(ClaimTypes.SerialNumber, new Random().Next(0,100000000).ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(request.TimeToLifeSec),
                Issuer = "http://santechx.com",
                Audience = "http://santechx.com",
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}

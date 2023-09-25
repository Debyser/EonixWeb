using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private const string SecretKey = "rC*pxkLZoOARGnLrl*HQW0*PmCRj5tnBMbbjagDOc4&My!JVWeYs^e0upKD4i19ljiaqDj%9zYb#f#PblDYM%L$x5!BdGMlpWGC";
        private const string jwttoken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOlsiRW9uaXhXZWJBcGkiLCJodHRwczovL2xvY2FsaG9zdDo3MjAxLyJdLCJhdWQiOlsid3d3LmVvbml4V2ViQXBpLmNvbSIsImh0dHBzOi8vbG9jYWxob3N0OjcyMDEvIl0sIm5iZiI6IjE2OTU2NDU1MjciLCJleHAiOiIxNjk1NzMxOTI3IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6IkRlYnlzZXIiLCJFbWFpbCI6Imphc29uLmRlYnlzZXJAZ21haWwuY29tIiwiUm9sZSI6IkRldmVsb3BlciJ9.Pq9y73eAwRe-_cLGwyOAXghIZgt5HM-yLQzcJ9WsCZ0";
        public static readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        [HttpPost]
        public async Task<IActionResult> Create() => new ObjectResult(await GenerateJwtToken());


        private async Task<dynamic> GenerateJwtToken()
        {
            var credentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);

            //TOken will be good for 1 minutes
            var payload = new JwtPayload("EonixWebApi", "www.eonixWebApi.com", null, null, null);
            var userName = "Debyser";

            payload.AddClaim(new Claim("iss", "https://localhost:7201/")); //Issuer - the party generating the JWT
            payload.AddClaim(new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()));
            payload.AddClaim(new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()));
            payload.AddClaim(new Claim("aud", "https://localhost:7201/"));  // Audience - The address of the resource
            payload.AddClaim(new Claim(ClaimTypes.Name, userName));
            payload.AddClaim(new Claim("Email", "jason.debyser@gmail.com"));
            payload.AddClaim(new Claim("Role", "Developer"));

            var header = new JwtHeader(credentials);
            var secToken = new JwtSecurityToken(header, payload);

            var output = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(secToken),
                UserName = userName
            };

            return await Task.FromResult<dynamic>(output);
        }
    }
}

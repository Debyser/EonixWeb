using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Helpers
{
    public static class JwtToken
    {
        private const string SecretKey = "rC*pxkLZoOARGnLrl*HQW0*PmCRj5tnBMbbjagDOc4&My!JVWeYs^e0upKD4i19ljiaqDj%9zYb#f#PblDYM%L$x5!BdGMlpWGC";
        private const string jwttoken = "";
        public static readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        public static string GenerateJwtToken()
        {
            var credentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);

            //TOken will be good for 1 minutes
            var payload = new JwtPayload("EonixWebApi", "www.eonixWebApi.com", null, null, null);

            payload.AddClaim(new Claim("iss", "https://localhost:7201/")); //Issuer - the party generating the JWT
            payload.AddClaim(new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()));
            payload.AddClaim(new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()));
            payload.AddClaim(new Claim("aud", "https://localhost:7201/"));  // Audience - The address of the resource
            payload.AddClaim(new Claim("sub", "Debyser Jason"));
            payload.AddClaim(new Claim("GivenName", "Jason"));
            payload.AddClaim(new Claim("Surname", "Debyser"));
            payload.AddClaim(new Claim("Email", "jason.debyser@gmail.com"));
            payload.AddClaim(new Claim("Role", "Developer"));

            var header = new JwtHeader(credentials);

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secToken);

            return tokenString;
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApi.Helpers
{
    public static class JwtToken
    {
        private const string SecretKey = "rC*pxkLZoOARGnLrl*HQW0*PmCRj5tnBMbbjagDOc4&My!JVWeYs^e0upKD4i19ljiaqDj%9zYb#f#PblDYM%L$x5!BdGMlpWGC";
        public static readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

    }
}

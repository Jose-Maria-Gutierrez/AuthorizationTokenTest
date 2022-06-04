using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestApis.Helpers;
using TestApis.Models;

namespace TestApis.Authorization
{
    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings _appSettings;

        public JwtUtils(IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
        }
        
        public string GenerateJwtToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }), //datos que contiene
                Expires = DateTime.UtcNow.AddHours(1), //tiempo de vida
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), //firma del token
                SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor); //crea el token
            return tokenHandler.WriteToken(token); //de SecurityToken a string
        }

        public int? ValidateJwtToken(string token)
        {
            if (token==null)
                return null;
            
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            try
            {
                tokenHandler.ValidateToken(token,new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = (JwtSecurityToken) validatedToken; //lo parsea
                int userId = int.Parse(jwtToken.Claims.First(x=>x.Type=="id").Value);
                //devuelve el id del usuario si el token es validado correctamente
                return userId;
            }
            catch //falla validacion
            {
                return null;
            }

        }
    }
}

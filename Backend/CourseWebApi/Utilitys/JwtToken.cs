using CourseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CourseWebApi.Utilitys
{
    public class JwtToken
    {
        private readonly IConfiguration _iConfiguration;

        public JwtToken(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
        }

        public string RefreshJwtTokenByAuto(string token)
        {
            try
            {
                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var nowTime = DateTimeOffset.Now.ToUnixTimeSeconds();

                if (jwtToken.Payload.TryGetValue("nbf", out object nbf) 
                    && jwtToken.Payload.TryGetValue("exp", out object exp) 
                    && jwtToken.Payload.TryGetValue(JwtRegisteredClaimNames.Sub, out object userId))
                {
                    //if exp has been over
                    if ((nowTime - (long)nbf) >= ((long)exp) - ((long)nbf))
                    {
                        return RefreshJwtToken(token);
                    }
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string RefreshJwtToken(string token)
        {
            try
            {
                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

                //get new Token
                if (jwtToken.Payload.TryGetValue(JwtRegisteredClaimNames.Sub, out object userId))
                {
                    var user = new User();
                    var newToken = CreateJwtToken(user);
                    return newToken;
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string CreateJwtToken(User user)
        {
            if (user == null)
            {
                return "";
            }

            // generate jwt
            var signInAlgorithm = SecurityAlgorithms.HmacSha256;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role)
            };

            var secretByte = Encoding.UTF8.GetBytes(_iConfiguration["Authentication:SecretKey"]);
            var signInKey = new SymmetricSecurityKey(secretByte);
            var signInCredentials = new SigningCredentials(signInKey, signInAlgorithm);

            var token = new JwtSecurityToken(
                    issuer: _iConfiguration["Authentication:Issuer"],
                    audience: _iConfiguration["Authentication:Audience"],
                    claims,
                    notBefore:DateTime.UtcNow,
                    expires:DateTime.UtcNow.AddMinutes(20),
                    signInCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

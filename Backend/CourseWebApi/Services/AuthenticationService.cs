using CourseModels;
using CourseWebApi.Utilitys;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWebApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _iConfiguration;

        public AuthenticationService(IConfiguration iConfiguration)
        {
            _iConfiguration = iConfiguration;
        }

        public string GenerateJwtToken(User user)
        {
            // generate  token of jwt
            var jwtToken = new JwtToken(_iConfiguration);
            var tokenStr = jwtToken.CreateJwtToken(user);

            if (string.IsNullOrWhiteSpace(tokenStr))
            {
                return "";
            }

            return tokenStr;
        }
    }
}

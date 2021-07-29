using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Services;

namespace WebApi.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IAuthService authService, AppContext db)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, authService, db, token);

            await _next(context);
        }

        private TokenValidationParameters GetValidationParameters()
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            };
        }

        public bool ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            return true;
        }

        private void attachUserToContext(HttpContext context, IAuthService authService, AppContext db, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                TokenValidationParameters parameters = GetValidationParameters();
                tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                //attach jwt to context on successful jwt validation
                context.Items["User"] = db.User.Single(u => u.Id == userId);
            }
            catch
            {
                // do nothing if jwt validation fails
                // jwt is not attached to context so request won't have access to secure routes
            }
        }
    }
}
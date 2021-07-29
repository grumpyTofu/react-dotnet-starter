using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Google.Apis.Auth;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models;
using WebApi;
using System.Diagnostics;

namespace WebApi.Services
{
    public interface IAuthService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }

    public class AuthService : IAuthService
    {
        private readonly AppSettings _appSettings;
        private readonly AppContext _context;

        public AuthService(IOptions<AppSettings> appSettings, AppContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest data)
        {
            GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings();
            settings.Audience = new List<string>() { "224817554491-jdvotflbjjf9aq9dmr2t7h2kkm7m8equ.apps.googleusercontent.com" };
            // secret = zsMFBrCcwS5zYXi74i_3_Fgb

            Debug.Write(data.IdToken);
            try
            {
                GoogleJsonWebSignature.Payload payload = GoogleJsonWebSignature.ValidateAsync(data.IdToken, settings).Result;

                Debug.Write(payload.Email);
                User user = _context.User.Single(u => u.Email == payload.Email);
                // var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

                // return null if user not found
                if (user == null)
                {
                    _context.User.Add(new User
                    {
                        Email = payload.Email,
                        FirstName = payload.GivenName,
                        LastName = payload.FamilyName
                    });
                };

                // authentication successful so generate jwt token
                var token = generateJwtToken(user);

                return new AuthenticateResponse(user, token);
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
                return null;
            }

        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
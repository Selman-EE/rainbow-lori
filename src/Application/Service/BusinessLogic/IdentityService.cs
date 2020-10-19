using Application.Common;
using Application.Model.Request;
using Application.Model.Response;
using Application.Service.Interfaces;
using Domain.Entities;
using Domain.Persistence;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.BusinessLogic
{
    public class IdentityService : IIdentityService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly JwtSettings _jwtSettings;

        public IdentityService(IApplicationDbContext dbContext, TokenValidationParameters tokenValidationParameters, JwtSettings jwtSettings)
        {
            _dbContext = dbContext;
            _tokenValidationParameters = tokenValidationParameters;
            _jwtSettings = jwtSettings;
        }

        public async Task<ResultExtended<UserRes>> RegisterAsync(UserReq userReq)
        {
            var existingUser = await _dbContext.Users.Where(x => x.EmailAddress == userReq.EmailAddress).FirstOrDefaultAsync();
            if (existingUser != null)
                return ResultExtended<UserRes>.Failure(null, new[] { "This email address already exists" });
            //
            existingUser = await _dbContext.Users.Where(x => x.Username == userReq.Username).FirstOrDefaultAsync();
            if (existingUser != null)
                return ResultExtended<UserRes>.Failure(null, new[] { "This username already exists" });

            var user = new User
            {
                Name = userReq.Name,
                Surname = userReq.Surname,
                Username = userReq.Username,
                EmailAddress = userReq.EmailAddress,
                Password = userReq.Password.Encrypt(),
            };
            //
            //
            _dbContext.Users.Add(user);
            var result = await _dbContext.SaveChangesAsync(new System.Threading.CancellationToken());
            if (result > 0)
            {
                var token = GenerateAccessToken(user.Id.ToString());
                return ResultExtended<UserRes>.Success(new UserRes
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Username = user.Username,
                    EmailAddress = user.EmailAddress,
                    AccessToken = token

                }, new[] { "Successful" });
            }
            else
                return ResultExtended<UserRes>.Failure(null, new[] { "This username address already exists" });
        }
        public async Task<ResultExtended<LoginRes>> LoginAsync(LoginReq loginReq)
        {
            User user = null;
            if (!string.IsNullOrWhiteSpace(loginReq.EmailAddress))
            {
                user = await _dbContext.Users.Where(x => x.EmailAddress == loginReq.EmailAddress).FirstOrDefaultAsync();
                if (user == null)
                    return ResultExtended<LoginRes>.Failure(null, new[] { "This email address does not exists" });
            }
            else
            {
                user = await _dbContext.Users.Where(x => x.Username == loginReq.Username).FirstOrDefaultAsync();
                if (user == null)
                    return ResultExtended<LoginRes>.Failure(null, new[] { "This username does not exists" });
            }
            //
            if (user?.Password.Decrypt() != loginReq.Password)
                return ResultExtended<LoginRes>.Failure(null, new[] { "Check your password please!" });
            //
            //
            var token = GenerateAccessToken(user.Id.ToString());
            return ResultExtended<LoginRes>.Success(new LoginRes
            {
                AccessToken = token
            }, new[] { "Successful" });
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                    return null;

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }

        internal string GenerateAccessToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, userId)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }


}

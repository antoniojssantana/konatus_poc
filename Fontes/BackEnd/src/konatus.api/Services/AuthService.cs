using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using konatus.api.Extensions;
using konatus.api.Interfaces;
using konatus.api.ViewModels;
using konatus.business.Notifications;
using konatus.business.Services;

namespace konatus.api.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly INotifier _notifier;

        public AuthService(INotifier notifier,
                                SignInManager<IdentityUser> signInManager,
                                UserManager<IdentityUser> userManager,
                                IOptions<AppSettings> appSettings,
                                IMapper mapper) : base(notifier)
        {
            _notifier = notifier;
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public Task<bool> ChangePassword(UserRegisterViewModel userRegister)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponseViewModel> Login(UserLoginViewModel userLogin)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RecoverPassword(UserRegisterViewModel userRegister)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseViewModel> RegisterUser(UserRegisterViewModel userRegister)
        {
            if (userRegister == null)
            {
                return null;
            }

            var user = new IdentityUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                var claims = _mapper.Map<IEnumerable<Claim>>(userRegister.Claims);
                //await _userManager.AddClaimsAsync(user, claims);
                await _signInManager.SignInAsync(user, false);

                var idUser = await _userManager.FindByEmailAsync(userRegister.Email);
                return await JwtGenerator(userRegister.Email);
            }

            foreach (var error in result.Errors)
            {
                _notifier.Handle(new Notification(error.Description));
            }
            return new LoginResponseViewModel();
        }

        private async Task<LoginResponseViewModel> JwtGenerator(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidOn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.HoursExpire),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encondedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = encondedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.HoursExpire).TotalSeconds,
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        public void Dispose()
        {
        }
    }
}
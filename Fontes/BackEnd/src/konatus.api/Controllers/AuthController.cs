using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using sga.utils;
using sga.utils.Interfaces;
using sga.utils.models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using konatus.api.Extensions;
using konatus.api.ViewModels;
using konatus.business.Notifications;

namespace konatus.api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _email;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public AuthController(INotifier notifier,
                                SignInManager<IdentityUser> signInManager,
                                UserManager<IdentityUser> userManager,
                                IOptions<AppSettings> appSettings,
                                 IEmailSender email,
                                 IHttpContextAccessor contextAccessor,
                                IMapper mapper) : base(notifier, contextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _mapper = mapper;
            _email = email;
        }

        [HttpPost("RegisterUser")]
        public async Task<ActionResult> RegisterUser(UserRegisterViewModel registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true,
            };

            var Claims = new List<ClaimViewModel>();
            Claims.Add(new ClaimViewModel { Type = "UserType", Value = "KonatusOperator" });
            registerUser.Claims = Claims;

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (result.Succeeded)
            {
                var claims = _mapper.Map<IEnumerable<Claim>>(registerUser.Claims);
                await _userManager.AddClaimsAsync(user, claims);
                await _signInManager.SignInAsync(user, false);

                var idUser = await _userManager.FindByEmailAsync(registerUser.Email);
                return CustomResponse(await JwtGenerator(registerUser.Email));
            }

            foreach (var error in result.Errors)
            {
                ErrorNotifier(error.Description);
            }
            return CustomResponse(registerUser);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserLoginViewModel>> Login(UserLoginViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return CustomResponse(await JwtGenerator(loginUser.Email));
            }
            if (result.IsLockedOut)
            {
                ErrorNotifier("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse(loginUser);
            }

            ErrorNotifier("Usuário ou Senha incorretors");
            return CustomResponse(loginUser);
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
    }
}
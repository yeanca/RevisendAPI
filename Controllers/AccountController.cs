using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RevisendAPI.Data;
using RevisendAPI.Data.BindingModel;
using RevisendAPI.Data.BindingModel.Enums;
using RevisendAPI.Data.Entities;
using RevisendAPI.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RevisendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTConfig _jWTConfig;
        private readonly ILogger<AccountController> _logger;
        private readonly RevisendAPIDbContext _context;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IOptions<JWTConfig> jWTConfig, ILogger<AccountController> logger, RevisendAPIDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jWTConfig = jWTConfig.Value;
            _logger = logger;
            _context = context;
        }

        //register a user
        [HttpPost("Register")]
        public async Task<object> RegisterUser([FromBody] AddUpdateRegisterUserBindingModel model)
        {
            try
            {
                var user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName= model.UserName,
                    AppId = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")),
                };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    var tempUser = await _userManager.FindByNameAsync(model.UserName);
                    //await _context.SaveChangesAsync();
                    //await _userManager.AddToRoleAsync(user, model.Role);
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Registration Successful", new UserDTO(userId: tempUser.Id, appId: tempUser.AppId, userName: tempUser.UserName, firstName: tempUser.FirstName, lastName:tempUser?.LastName, email: tempUser?.Email, model.Role,"token here")));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "", result.Errors.Select(x => x.Description).ToArray()));
            }
            catch (Exception ex)
            {

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }


        //register a user with google
        [HttpPost("RegisterEx")]
        public async Task<object> ExternalLogin(string provider,string returnUrl)
        {
            try
            {
                var redirectUrl = Url.Action("ExternalLoginCallback", "Account",new { ReturnUrl = returnUrl });

                var properties =_signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

                return new ChallengeResult(provider, properties);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginBindingModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                    if (result.Succeeded)
                    {
                        var userF = await _userManager.FindByNameAsync(model.UserName);
                        var role = (await _userManager.GetRolesAsync(userF)).FirstOrDefault();
                        var user = new UserDTO(userId: userF.Id, appId: userF.AppId, userName: userF.UserName, firstName: userF.FirstName, lastName: userF?.LastName, email: userF?.Email, role,"token");
                        user.Token = GenerateToken(userF, role);
                        return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Login Successful!", user));
                    }
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Invalid Login Credentials!", null));

            }
            catch (Exception ex)
            {

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }


        private string GenerateToken(User user, string role)
        {
            var jwTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(JwtRegisteredClaimNames.NameId,user.Id),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role,role),
                }),
                Expires = DateTime.Now.AddHours(72),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jWTConfig.Audience,
                Issuer = _jWTConfig.Issuer
            };
            var token = jwTokenHandler.CreateToken(tokenDescriptor);
            return jwTokenHandler.WriteToken(token);
        }

    }
}
